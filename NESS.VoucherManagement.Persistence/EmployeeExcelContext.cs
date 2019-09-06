using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NESS.VoucherManagement.Persistence.Model;
using Npoi.Mapper;

namespace NESS.VoucherManagement.Persistence
{
	public class EmployeeExcelContext : IExcelContext
	{
		private readonly string businessTripsFilePath;

		private readonly string employeesFilePath;

		private readonly string timesheetsFilePath;

		private Mapper businessTripsMapper;

		private Mapper employeesMapper;

		private Mapper timesheetMapper;

		/// <summary>
		/// Creates an instance of the class.
		/// </summary>
		/// <param name="employeesFilePath"></param>
		/// <param name="timesheetsFilePath"></param>
		/// <param name="businessTripsFilePath"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public EmployeeExcelContext(string employeesFilePath, string timesheetsFilePath, string businessTripsFilePath)
		{
			if (employeesFilePath == null) throw new ArgumentNullException(nameof(employeesFilePath));
			if (timesheetsFilePath == null) throw new ArgumentNullException(nameof(timesheetsFilePath));
			if (businessTripsFilePath == null) throw new ArgumentNullException(nameof(businessTripsFilePath));

			if (string.IsNullOrWhiteSpace(employeesFilePath)) throw new ArgumentException("Value must not be empty.", nameof(employeesFilePath));
			if (string.IsNullOrWhiteSpace(timesheetsFilePath)) throw new ArgumentException("Value must not be empty.", nameof(timesheetsFilePath));
			if (string.IsNullOrWhiteSpace(businessTripsFilePath)) throw new ArgumentException("Value must not be empty.", nameof(businessTripsFilePath));

			this.employeesFilePath = employeesFilePath;
			this.timesheetsFilePath = timesheetsFilePath;
			this.businessTripsFilePath = businessTripsFilePath;
		}

		/// <summary>
		///     Exposes a time sheet collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		public IEnumerable<ExcelTimesheet> Timesheets
		{
			get
			{
				EnsureTimesheetMapperCreated();

				return timesheetMapper.Take<ExcelTimesheet>()
					.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
					.Select(x => x.Value);
			}
		}

		/// <summary>
		///     Exposes an employee collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		public IEnumerable<ExcelEmployee> Employees
		{
			get
			{
				EnsureEmployeesMapperCreated();

				return employeesMapper.Take<ExcelEmployee>()
					.Where(x => !string.IsNullOrWhiteSpace(x.Value.SapId))
					.Select(x => x.Value);
			}
		}

		/// <summary>
		///     Exposes a business trip collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		public IEnumerable<ExcelBusinessTrip> BusinessTrips
		{
			get
			{
				EnsureBusinessTripMapperCreated();

				return businessTripsMapper.Take<ExcelBusinessTrip>()
					.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
					.Select(x => x.Value);
			}
		}

		private void EnsureBusinessTripMapperCreated()
		{
			if (businessTripsMapper != null) return;

			try
			{
				businessTripsMapper = new Mapper(businessTripsFilePath)
					.Map<ExcelBusinessTrip>(0, x => x.CompanyCode)
					.Map<ExcelBusinessTrip>(1, x => x.EmployeeSapId)
					.Map<ExcelBusinessTrip>(2, x => x.Name)
					.Map<ExcelBusinessTrip>(3, x => x.DaysInDelegation);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(businessTripsFilePath, "Expecting an Excel file.", ex);
			}
		}

		private void EnsureEmployeesMapperCreated()
		{
			if (employeesMapper != null) return;
			try
			{
				employeesMapper = new Mapper(employeesFilePath)
					.Map<ExcelEmployee>("Pers.no.", x => x.SapId)
					.Map<ExcelEmployee>("Last name", x => x.LastName)
					.Map<ExcelEmployee>("First name", x => x.FirstName)
					.Map<ExcelEmployee>("ID number", x => x.PersonalId);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(employeesFilePath, "Expecting an Excel file.", ex);
			}
		}

		private void EnsureTimesheetMapperCreated()
		{
			if (timesheetMapper != null) return;

			try
			{
				timesheetMapper = new Mapper(timesheetsFilePath)
					.Map<ExcelTimesheet>(0, x => x.EmployeeSapId)
					.Map<ExcelTimesheet>(1, x => x.EmployeeName)
					.Map<ExcelTimesheet>(2, x => x.OperationId)
					.Map<ExcelTimesheet>(2, x => x.OperationDescription)
					.Map<ExcelTimesheet>(6, x => x.Date).Format<ExcelTimesheet>("dd.MM.yyyy", t => t.Date);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(timesheetsFilePath, "Expecting an Excel file.", ex);
			}
		}
	}
}