namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Application;

	public interface IWeekDaysProvider
	{
		IEnumerable<DateTime> GetWeekDays(When when);

		Task<IEnumerable<DateTime>> GetWeekDaysAsync(When when);
	}
}