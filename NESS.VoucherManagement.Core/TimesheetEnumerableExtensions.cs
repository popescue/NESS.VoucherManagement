using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Domain;

namespace NESS.VoucherManagement.Core
{
	internal static class TimesheetEnumerableExtensions
	{
		public static int OutOfOfficeCount(this IEnumerable<Timesheet> timesheets, IEnumerable<Operation> outOfOfficeOperations)
		{
			return timesheets.Count(x => outOfOfficeOperations.Contains(x.Operation));
		}
	}
}