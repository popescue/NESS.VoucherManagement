using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Domain
{
	public class LunchTicketInfo
	{
		public const decimal Value = 15M;

		public LunchTicketInfo(Employee employee, int count)
		{
			Employee = employee;
			Count = count;
		}

		public Employee Employee { get; }

		public int Count { get; }
	}
}