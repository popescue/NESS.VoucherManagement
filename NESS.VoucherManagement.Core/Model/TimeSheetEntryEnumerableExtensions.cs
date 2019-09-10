namespace NESS.VoucherManagement.Core.Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal static class TimeSheetEntryEnumerableExtensions
	{
		public static int OutOfOfficeCount(this IEnumerable<TimeSheetEntry> timeSheets, IEnumerable<Operation> outOfOfficeOperations)
			=> timeSheets.Count(x => outOfOfficeOperations.Contains(x.Operation));
	}
}