using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NESS.VoucherManagement.Models;

namespace NESS.VoucherManagement.ViewModels
{
	public class WorkingPeriodViewModel
	{
		public WorkingPeriodViewModel(int year)
		{
			Year = year;
			// ReSharper disable once PossibleNullReferenceException
			AvailableMonths = DateTimeFormatInfo.CurrentInfo.MonthNames.Zip(Enumerable.Range(1, 12), (name, index) => new Month(index, name));
			Month = AvailableMonths.Single(x => x.Index == DateTime.Now.Month);
		}

		public int Year { get; set; }

		public Month Month { get; set; }

		public IEnumerable<Month> AvailableMonths { get; set; }
	}
}