using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Domain
{
	public class LunchTicketInfo
	{
		public const decimal Value = 15M;

		public LunchTicketInfo(int count)
		{
			if (count < 0) throw new ArgumentException("Value must be a positive integer.", nameof(count));

			Count = count;
		}

		public int Count { get; }
	}
}