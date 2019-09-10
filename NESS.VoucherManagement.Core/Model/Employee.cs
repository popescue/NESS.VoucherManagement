using System;
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
			TimeSheetEntries = timesheets ?? throw new ArgumentNullException(nameof(timesheets));
			BusinessTrips = businessTrips;
			FirstName = firstName;
			LastName = lastName;
			PersonalId = personalId;
			SapId = sapId;
		}

		public IEnumerable<BusinessTrip> BusinessTrips { get; }

		public IEnumerable<TimeSheetEntry> TimeSheetEntries { get; }

		public Voucher CalculateVouchers(int workingDaysThisMonth, IEnumerable<Operation> outOfOfficeOperations)
		{
			var workedDays = TimeSheetEntries.GroupBy(t => t.Date).Count();
			
			return new Voucher
			(
				this,
				Math.Min(workingDaysThisMonth, workedDays)
				- TimeSheetEntries.OutOfOfficeCount(outOfOfficeOperations)
				- BusinessTrips.Sum(x => x.Days)
			);
		}
	}
}