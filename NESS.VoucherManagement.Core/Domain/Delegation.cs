using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Domain
{
	public class Delegation
	{
		public Delegation(int daysInDelegation)
		{
			if (daysInDelegation < 0) throw new ArgumentException("Value must be a positive integer.", nameof(daysInDelegation));

			DaysInDelegation = daysInDelegation;
		}

		public int DaysInDelegation { get; }

		public static Delegation NoDelegation() => new Delegation(0);
	}
}