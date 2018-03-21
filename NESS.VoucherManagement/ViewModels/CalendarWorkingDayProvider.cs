using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Application;

namespace NESS.VoucherManagement.ViewModels
{
	public class CalendarWorkingDayProvider : IWorkingDayProvider
	{
		private readonly IHolidayProvider holidayProvider;

		public CalendarWorkingDayProvider(IHolidayProvider holidayProvider) => this.holidayProvider = holidayProvider;

		public int Count(int year, int month)
			=> GenerateWeekDays(year, month)
				.Except(holidayProvider.GetHolidays(year, month))
				.Count();

		private static IEnumerable<DateTime> GenerateWeekDays(int year, int month)
			=> Enumerable.Range(1, DateTime.DaysInMonth(year, month))
			             .Select(d => new DateTime(year, month, d))
			             .Where(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);
	}
}