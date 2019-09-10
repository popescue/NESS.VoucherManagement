namespace NESS.VoucherManagement.Calendar
{
	using System;
	using System.Linq;

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

		public int WorkingDaysForMonth(MonthYear monthYear)
		{
			var holidays = holidayProvider.GetHolidays(monthYear);

			return weekDaysProvider.Count(monthYear, holidays);
		}
	}
}