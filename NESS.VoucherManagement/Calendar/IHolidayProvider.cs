using System;
using System.Linq;

namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Application;

	public interface IHolidayProvider
	{
		IEnumerable<DateTime> GetHolidays(MonthYear my);
	}
}