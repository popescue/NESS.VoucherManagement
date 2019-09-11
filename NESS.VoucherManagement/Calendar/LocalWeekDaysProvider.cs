namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Application;

	public class LocalWeekDaysProvider : IWeekDaysProvider
	{
		public IEnumerable<DateTime> GetWeekDays(When when)
			=> WeekDays(when.Year, when.Month);

		public Task<IEnumerable<DateTime>> GetWeekDaysAsync(When when)
			=> Task.FromResult(WeekDays(when.Year, when.Month));

		private static IEnumerable<DateTime> WeekDays(int year, int month)
			=> Enumerable.Range(1, DateTime.DaysInMonth(year, month))
				.Select(d => new DateTime(year, month, d))
				.Where(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);
	}
}