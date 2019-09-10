namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using Model;

	using Npoi.Mapper;

	public class EmployeeExcelContext : IContext
	{
		private readonly Mapper businessTripsMapper;

		private readonly Mapper employeesMapper;

		private readonly Mapper timeSheetMapper;

		/// <summary>
		///     Creates an instance of the class.
		/// </summary>
		/// <param name="employeesFilePath"></param>
		/// <param name="timeSheetsFilePath"></param>
		/// <param name="businessTripsFilePath"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		public EmployeeExcelContext(string employeesFilePath, string timeSheetsFilePath, string businessTripsFilePath)
		{
			if (employeesFilePath == null) throw new ArgumentNullException(nameof(employeesFilePath));
			if (timeSheetsFilePath == null) throw new ArgumentNullException(nameof(timeSheetsFilePath));
			if (businessTripsFilePath == null) throw new ArgumentNullException(nameof(businessTripsFilePath));

			if (string.IsNullOrWhiteSpace(employeesFilePath)) throw new ArgumentException("Value must not be empty.", nameof(employeesFilePath));
			if (string.IsNullOrWhiteSpace(timeSheetsFilePath)) throw new ArgumentException("Value must not be empty.", nameof(timeSheetsFilePath));
			if (string.IsNullOrWhiteSpace(businessTripsFilePath)) throw new ArgumentException("Value must not be empty.", nameof(businessTripsFilePath));

			employeesMapper = CreateEmployeesMapper(employeesFilePath);
			timeSheetMapper = CreateTimeSheetMapper(timeSheetsFilePath);
			businessTripsMapper = CreateBusinessTripsMapper(businessTripsFilePath);
		}

		/// <summary>
		///     Exposes an employee collection.
		/// </summary>
		public IEnumerable<ExcelTimeSheetEntry> GetTimeSheetEntries()
		{
			return timeSheetMapper.Take<ExcelTimeSheetEntry>()
				.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
				.Select(x => x.Value);
		}

		/// <summary>
		///     Exposes a time sheet collection.
		/// </summary>
		public IEnumerable<ExcelEmployee> GetEmployees()
		{
			return employeesMapper.Take<ExcelEmployee>()
				.Where(x => !string.IsNullOrWhiteSpace(x.Value.SapId))
				.Select(x => x.Value);
		}

		/// <summary>
		///     Exposes a business trip collection.
		/// </summary>
		public IEnumerable<ExcelBusinessTrip> GetBusinessTrips()
		{
			return businessTripsMapper.Take<ExcelBusinessTrip>()
				.Where(x => !string.IsNullOrWhiteSpace(x.Value.EmployeeSapId))
				.Select(x => x.Value);
		}

		/// <summary>
		///     Instantiates a mapper to read business trips.
		/// </summary>
		/// <param name="path"></param>
		/// <exception cref="InvalidFileTypeException"></exception>
		private static Mapper CreateBusinessTripsMapper(string path)
		{
			try
			{
				var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				return new Mapper(fs)
					.Map<ExcelBusinessTrip>(0, x => x.CompanyCode)
					.Map<ExcelBusinessTrip>(1, x => x.EmployeeSapId)
					.Map<ExcelBusinessTrip>(2, x => x.Name)
					.Map<ExcelBusinessTrip>(3, x => x.DaysInBusinessTrip);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(path, "Expecting an Excel file.", ex);
			}
		}

		/// <summary>
		///     Instantiates a mapper to read employee information.
		/// </summary>
		/// <param name="path"></param>
		/// <exception cref="InvalidFileTypeException"></exception>
		private static Mapper CreateEmployeesMapper(string path)
		{
			try
			{
				var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				return new Mapper(fs)
					.Map<ExcelEmployee>(0, x => x.SapId)
					.Map<ExcelEmployee>(2, x => x.LastName)
					.Map<ExcelEmployee>(3, x => x.FirstName)
					.Map<ExcelEmployee>(4, x => x.PersonalId);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(path, "Expecting an Excel file.", ex);
			}
		}

		/// <summary>
		///     Instantiates a mapper to read time-sheet information.
		/// </summary>
		/// <param name="path"></param>
		/// <exception cref="InvalidFileTypeException"></exception>
		private static Mapper CreateTimeSheetMapper(string path)
		{
			try
			{
				var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				return new Mapper(fs)
					.Map<ExcelTimeSheetEntry>(0, x => x.EmployeeSapId)
					.Map<ExcelTimeSheetEntry>(2, x => x.OperationId)
					.Map<ExcelTimeSheetEntry>(2, x => x.OperationDescription)
					.Map<ExcelTimeSheetEntry>(6, x => x.Date).Format<ExcelTimeSheetEntry>("dd.MM.yyyy", t => t.Date);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidFileTypeException(path, "Expecting an Excel file.", ex);
			}
		}
	}
}