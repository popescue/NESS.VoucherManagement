namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Model;

	public interface IContext
	{
		/// <summary>
		///     Exposes an employee collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		IEnumerable<ExcelEmployee> GetEmployees();

		/// <summary>
		///     Exposes a business trip collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		IEnumerable<ExcelBusinessTrip> GetBusinessTrips();

		/// <summary>
		///     Exposes a time sheet collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		IEnumerable<ExcelTimeSheetEntry> GetTimeSheetEntries();
	}
}