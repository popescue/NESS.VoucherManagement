using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Model
{
	public struct TimeSheet
	{
		public TimeSheet(Operation operation, DateTime date)
		{
			Operation = operation;
			Date = date;
		}

		public Operation Operation { get; }

		public DateTime Date { get; }
	}
}