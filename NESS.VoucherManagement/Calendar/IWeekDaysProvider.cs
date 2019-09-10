namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Application;

	public interface IWeekDaysProvider
	{
		int Count(MonthYear my, IEnumerable<DateTime> holidays);
	}
}