using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Domain
{
	public class VoucherInfo
	{
		public const decimal Value = 15M;

		public VoucherInfo(Employee employee, int count)
		{
			if (count < 0) throw new ArgumentException("Value must be a positive integer.", nameof(count));

			Employee = employee ?? throw new ArgumentNullException(nameof(employee));
			Count = count;
		}

		public int Count { get; }

		public Employee Employee { get; }
	}
}