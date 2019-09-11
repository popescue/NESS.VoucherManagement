namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Model;

	public interface IReadContext
	{
		IEnumerable<ExcelEmployee> Employees { get; }

		IEnumerable<ExcelBusinessTrip> BusinessTrips { get; }

		IEnumerable<ExcelTimeSheetEntry> TimeSheetEntries { get; }

		Task<IEnumerable<ExcelEmployee>> GetEmployeesAsync();

		Task<IEnumerable<ExcelBusinessTrip>> GetBusinessTripsAsync();

		Task<IEnumerable<ExcelTimeSheetEntry>> GetTimeSheetEntriesAsync();
	}
}