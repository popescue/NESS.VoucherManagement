﻿namespace NESS.VoucherManagement.ViewModels
{
	using System;
	using System.Linq;

	public struct MonthYear
	{
		public int Year { get; }

		public int Month { get; }

		public MonthYear(int year, int month)
		{
			Year = year;
			Month = month;
		}
	}
}