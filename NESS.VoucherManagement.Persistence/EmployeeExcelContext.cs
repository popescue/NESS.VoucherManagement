using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
		/// </summary>
		/// <param name="employeesFilePath"></param>
		/// <param name="timesheetsFilePath"></param>
		/// <param name="businessTripsFilePath"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="FileNotFoundException"></exception>
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

			InitializeMappers();
		}

		public IEnumerable<ExcelTimesheet> Timesheets => timesheetMapper.Take<ExcelTimesheet>()
			.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
			.Select(x => x.Value);

		public IEnumerable<ExcelEmployee> Employees => employeesMapper.Take<ExcelEmployee>()
			.Where(x => !string.IsNullOrWhiteSpace(x.Value.SapId))
			.Select(x => x.Value);

		public IEnumerable<ExcelBusinessTrip> BusinessTrips => businessTripsMapper.Take<ExcelBusinessTrip>()
			.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
			.Select(x => x.Value);

		private void InitializeMappers()
		{
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

			try
			{
				businessTripsMapper = new Mapper(businessTripsFilePath)
					.Map<ExcelBusinessTrip>("Company code", x => x.CompanyCode)
					.Map<ExcelBusinessTrip>("SAP ID", x => x.EmployeeSapId)
					.Map<ExcelBusinessTrip>("Nume Prenume", x => x.Name)
					.Map<ExcelBusinessTrip>(3, x => x.DaysInDelegation);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(businessTripsFilePath, "Expecting an Excel file.", ex);
			}

			try
			{
				timesheetMapper = new Mapper(timesheetsFilePath)
					.Map<ExcelTimesheet>("Pers.No.", x => x.EmployeeSapId)
					.Map<ExcelTimesheet>("Name", x => x.EmployeeName)
					.Map<ExcelTimesheet>("OpAc", x => x.OperationId)
					.Map<ExcelTimesheet>("Ac.Descr.", x => x.OperationDescription)
					.Map<ExcelTimesheet>("Date", x => x.Date).Format<ExcelTimesheet>("dd.MM.yyyy", t => t.Date);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(timesheetsFilePath, "Expecting an Excel file.", ex);
			}
		}
	}

	[Serializable]
	public class InvalidFileTypeException : Exception
	{
		private const string DefaultMessage = "The file was not of the expected type.";

		public InvalidFileTypeException(string filePath)
			: base(DefaultMessage) => FilePath = filePath;

		public InvalidFileTypeException(string filePath, string message)
			: base(message) => FilePath = filePath;

		public InvalidFileTypeException(string filePath, string message, Exception inner)
			: base(message, inner) => FilePath = filePath;

		protected InvalidFileTypeException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }

		public string FilePath { get; }
	}
}