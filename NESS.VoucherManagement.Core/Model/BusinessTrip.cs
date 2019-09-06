namespace NESS.VoucherManagement.Core.Model
{
	using System;
	using System.Linq;

	public struct BusinessTrip
	{
		public BusinessTrip(int days)
		{
			if (days < 0) throw new ArgumentException("Value must be a positive integer.", nameof(days));

			Days = days;
		}

		public int Days { get; }
	}
}