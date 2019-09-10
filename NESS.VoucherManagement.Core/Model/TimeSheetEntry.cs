namespace NESS.VoucherManagement.Core.Model
{
	using System;
	using System.Linq;

	public struct TimeSheetEntry
	{
		public TimeSheetEntry(string operation, DateTime date)
		{
			Operation = operation;
			Date = date;
		}

		public string Operation { get; }

		public DateTime Date { get; }
	}
}