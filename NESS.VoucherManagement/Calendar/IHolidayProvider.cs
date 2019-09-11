namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Application;

	public interface IHolidayProvider
	{
		IEnumerable<DateTime> GetHolidays(When when);

		Task<IEnumerable<DateTime>> GetHolidaysAsync(When when);
	}
}