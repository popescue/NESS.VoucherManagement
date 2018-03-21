using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Application
{
	public interface IHolidayProvider {
		IEnumerable<DateTime> GetHolidays(int year, int month);
	}
}