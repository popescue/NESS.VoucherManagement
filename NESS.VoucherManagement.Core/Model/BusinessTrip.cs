using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Model
{
	public struct BusinessTrip
	{
		public BusinessTrip(int days)
		{
			if (days < 0) throw new ArgumentException("Value must be a positive integer.", nameof(days));

			Days = days;
		}

		public int Days { get; }

		public static BusinessTrip NoDelegation() => new BusinessTrip(0);
	}
}