using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NESS.VoucherManagement.Persistence.Model;

namespace NESS.VoucherManagement.Persistence
{
	public interface IExcelContext
	{
		/// <summary>
		///     Exposes an employee collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		IEnumerable<ExcelEmployee> Employees { get; }

		/// <summary>
		///     Exposes a business trip collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		IEnumerable<ExcelBusinessTrip> BusinessTrips { get; }

		/// <summary>
		///     Exposes a time sheet collection.
		/// </summary>
		/// <exception cref="FileNotFoundException"></exception>
		/// <exception cref="InvalidFileTypeException"></exception>
		IEnumerable<ExcelTimesheet> Timesheets { get; }
	}
}