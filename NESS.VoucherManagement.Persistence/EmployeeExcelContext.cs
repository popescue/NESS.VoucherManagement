using System;
using System.Linq;

namespace NESS.VoucherManagement.Persistence
{
	using System.Collections.Generic;
	using System.IO;
	using Model;
	using Npoi.Mapper;

	public class EmployeeExcelContext : IContext
	{
		private readonly string businessTripsFilePath;

		private readonly string employeesFilePath;

		private readonly string timeSheetsFilePath;

		/// <summary>
		///     Creates an instance of the class.
		/// </summary>
		/// <param name="employeesFilePath"></param>
		/// <param name="timeSheetsFilePath"></param>
		/// <param name="businessTripsFilePath"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public EmployeeExcelContext(string employeesFilePath, string timeSheetsFilePath, string businessTripsFilePath)
		{
			if (employeesFilePath == null) throw new ArgumentNullException(nameof(employeesFilePath));
			if (timeSheetsFilePath == null) throw new ArgumentNullException(nameof(timeSheetsFilePath));
			if (businessTripsFilePath == null) throw new ArgumentNullException(nameof(businessTripsFilePath));

			if (string.IsNullOrWhiteSpace(employeesFilePath)) throw new ArgumentException("Value must not be empty.", nameof(employeesFilePath));
			if (string.IsNullOrWhiteSpace(timeSheetsFilePath)) throw new ArgumentException("Value must not be empty.", nameof(timeSheetsFilePath));
			if (string.IsNullOrWhiteSpace(businessTripsFilePath)) throw new ArgumentException("Value must not be empty.", nameof(businessTripsFilePath));

			this.employeesFilePath = employeesFilePath;
			this.timeSheetsFilePath = timeSheetsFilePath;
			this.businessTripsFilePath = businessTripsFilePath;
		}

		/// <summary>
		///     Exposes a time sheet collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		public IEnumerable<ExcelTimeSheetEntry> GetTimeSheetEntries()
		{
			try
			{
				var timesheetMapper = new Mapper(timeSheetsFilePath)
					.Map<ExcelTimeSheetEntry>(0, x => x.EmployeeSapId)
					.Map<ExcelTimeSheetEntry>(2, x => x.OperationId)
					.Map<ExcelTimeSheetEntry>(2, x => x.OperationDescription)
					.Map<ExcelTimeSheetEntry>(6, x => x.Date).Format<ExcelTimeSheetEntry>("dd.MM.yyyy", t => t.Date);

				return timesheetMapper.Take<ExcelTimeSheetEntry>()
					.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
					.Select(x => x.Value)
					.ToList();
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(timeSheetsFilePath, "Expecting an Excel file.", ex);
			}
			catch (IOException ex)
			{
				throw new FileInUseException(timeSheetsFilePath, ex);
			}
		}

		/// <summary>
		///     Exposes an employee collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		public IEnumerable<ExcelEmployee> GetEmployees()
		{
			try
			{
				var employeesMapper = new Mapper(employeesFilePath)
					.Map<ExcelEmployee>(0, x => x.SapId)
					.Map<ExcelEmployee>(2, x => x.LastName)
					.Map<ExcelEmployee>(3, x => x.FirstName)
					.Map<ExcelEmployee>(4, x => x.PersonalId);

				return employeesMapper.Take<ExcelEmployee>()
					.Where(x => !string.IsNullOrWhiteSpace(x.Value.SapId))
					.Select(x => x.Value)
					.ToList();
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(employeesFilePath, "Expecting an Excel file.", ex);
			}
			catch (IOException ex)
			{
				throw new FileInUseException(employeesFilePath, ex);
			}
		}

		/// <summary>
		///     Exposes a business trip collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		public IEnumerable<ExcelBusinessTrip> GetBusinessTrips()
		{
			try
			{
				var businessTripsMapper = new Mapper(businessTripsFilePath)
					.Map<ExcelBusinessTrip>(0, x => x.CompanyCode)
					.Map<ExcelBusinessTrip>(1, x => x.EmployeeSapId)
					.Map<ExcelBusinessTrip>(2, x => x.Name)
					.Map<ExcelBusinessTrip>(3, x => x.DaysInBusinessTrip);

				return businessTripsMapper.Take<ExcelBusinessTrip>()
					.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
					.Select(x => x.Value)
					.ToList();
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(businessTripsFilePath, "Expecting an Excel file.", ex);
			}
			catch (IOException ex)
			{
				throw new FileInUseException(businessTripsFilePath, ex);
			}
		}
	}
}