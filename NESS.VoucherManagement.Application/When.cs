namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Linq;

	public struct When
	{
		public int Year { get; }

		public int Month { get; }

		public When(int year, int month)
		{
			Year = year;
			Month = month;
		}
	}
}