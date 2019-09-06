namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IHolidayProvider
	{
		IEnumerable<DateTime> GetHolidays(int year, int month);
	}
}