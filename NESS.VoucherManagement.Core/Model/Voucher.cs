using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Model
{
	public class Voucher
	{
		public const decimal Value = 15m;

		public Voucher(Employee employee, int count)
		{
			if (count < 0) throw new ArgumentException("Value must be a positive integer.", nameof(count));

			Employee = employee ?? throw new ArgumentNullException(nameof(employee));
			Count = count;
		}

		public int Count { get; }

		public Employee Employee { get; }
	}
}