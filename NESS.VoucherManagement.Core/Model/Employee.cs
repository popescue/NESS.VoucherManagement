using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Model
{
	using System.Collections.Generic;

	public class Employee
	{
		// ReSharper disable once TooManyDependencies
		public Employee(string firstName, string lastName, string personalId, string sapId, IEnumerable<TimeSheetEntry> timesheets, IEnumerable<BusinessTrip> businessTrips)
		{
			TimeSheetEntries = timesheets ?? throw new ArgumentNullException(nameof(timesheets));
			BusinessTrips = businessTrips;
			FirstName = firstName;
			LastName = lastName;
			PersonalId = personalId;
			SapId = sapId;
		}

		public string FirstName { get; }

		public string LastName { get; }

		public string PersonalId { get; }

		public string SapId { get; }

		public IEnumerable<BusinessTrip> BusinessTrips { get; }

		public IEnumerable<TimeSheetEntry> TimeSheetEntries { get; }

		public Voucher CalculateVouchers(int workingDaysThisMonth, IEnumerable<string> outOfOfficeOperations)
		{
			var daysEligibleForVouchers = Math.Min(workingDaysThisMonth, TimeSheetEntries.DaysOnPayroll());
			var daysOutOfOffice = TimeSheetEntries.DaysOutOfOffice(outOfOfficeOperations);
			var daysOnTrips = BusinessTrips.DaysOnTrips();

			return new Voucher
			(
				this,
				daysEligibleForVouchers - daysOutOfOffice - daysOnTrips
			);
		}
	}
}