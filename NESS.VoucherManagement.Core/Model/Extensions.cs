namespace NESS.VoucherManagement.Core.Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal static class Extensions
	{
		public static int DaysOutOfOffice(this IEnumerable<TimeSheetEntry> timeSheets, IEnumerable<string> outOfOfficeOperations)
			=> timeSheets.Count(x => outOfOfficeOperations.Contains(x.Operation));

		public static int DaysOnPayroll(this IEnumerable<TimeSheetEntry> timeSheets)
			=> timeSheets.GroupBy(t => t.Date).Count();

		public static int DaysOnTrips(this IEnumerable<BusinessTrip> businessTrips)
			=> businessTrips.Sum(x => x.Days);
	}
}