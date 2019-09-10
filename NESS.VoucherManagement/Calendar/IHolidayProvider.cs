namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Application;

	public interface IHolidayProvider
	{
		IEnumerable<DateTime> GetHolidays(When when);
	}
}