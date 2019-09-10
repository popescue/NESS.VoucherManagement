namespace NESS.VoucherManagement.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;

	using Application;

	using Models;

	public class WorkingPeriodViewModel
	{
		public WorkingPeriodViewModel(MonthYear monthYear)
		{
			Year = monthYear.Year;

			// ReSharper disable once PossibleNullReferenceException
			AvailableMonths = DateTimeFormatInfo.CurrentInfo.MonthNames
				.Zip(Enumerable.Range(1, 12), (name, index) => new Month(index, name));

			Month = AvailableMonths.Single(x => x.Index == monthYear.Month);
		}

		public MonthYear MonthYear => new MonthYear(Year, Month.Index);

		public int Year { get; set; }

		public Month Month { get; set; }

		public IEnumerable<Month> AvailableMonths { get; set; }
	}
}