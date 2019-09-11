namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using Application;

	public class WorkingDaysProvider : IWorkingDaysProvider
	{
		private readonly IHolidayProvider holidayProvider;

		private readonly IWeekDaysProvider weekDaysProvider;

		public WorkingDaysProvider(IHolidayProvider holidayProvider, IWeekDaysProvider weekDaysProvider)
		{
			this.holidayProvider = holidayProvider;
			this.weekDaysProvider = weekDaysProvider;
		}

		public int GetWorkingDays(When when)
		{
			var holidays = holidayProvider.GetHolidays(when);

			var weekDays = weekDaysProvider.GetWeekDays(when);

			return weekDays.Except(holidays).Count();
		}

		public async Task<int> GetWorkingDaysAsync(When when)
		{
			var holidays = await holidayProvider.GetHolidaysAsync(when).ConfigureAwait(false);

			var weekDays = weekDaysProvider.GetWeekDays(when);

			return weekDays.Except(holidays).Count();
		}
	}
}