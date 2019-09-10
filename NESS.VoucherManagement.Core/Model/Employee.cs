﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Core.Model
{
	public class Employee
	{
		public string FirstName { get; }

		public string LastName { get; }

		public string PersonalId { get; }

		public string SapId { get; }

		public Employee(string firstName, string lastName, string personalId, string sapId, IEnumerable<TimeSheetEntry> timesheets, IEnumerable<BusinessTrip> businessTrips)
		{
			TimeSheets = timesheets ?? throw new ArgumentNullException(nameof(timesheets));
			BusinessTrips = businessTrips;
			FirstName = firstName;
			LastName = lastName;
			PersonalId = personalId;
			SapId = sapId;
		}

		public IEnumerable<BusinessTrip> BusinessTrips { get; }

		public IEnumerable<TimeSheetEntry> TimeSheets { get; }

		public Voucher CalculateVouchers(int workingDaysThisMonth, IEnumerable<Operation> outOfOfficeOperations)
		{
			var workedDays = TimeSheets.GroupBy(t => t.Date).Count();
			
			return new Voucher
			(
				this,
				Math.Min(workingDaysThisMonth, workedDays)
				- TimeSheets.OutOfOfficeCount(outOfOfficeOperations)
				- BusinessTrips.Sum(x => x.Days)
			);
		}
	}

	internal static class TimeSheetEnumerableExtensions
	{
		public static int OutOfOfficeCount(this IEnumerable<TimeSheetEntry> timeSheets, IEnumerable<Operation> outOfOfficeOperations) 
			=> timeSheets.Count(x => outOfOfficeOperations.Contains(x.Operation));
	}
}