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

		public Employee(string firstName, string lastName, string personalId, string sapId, IEnumerable<Timesheet> timesheets, IEnumerable<BusinessTrip> businessTrips)
		{
			Timesheets = timesheets ?? throw new ArgumentNullException(nameof(timesheets));
			BusinessTrips = businessTrips;
			FirstName = firstName;
			LastName = lastName;
			PersonalId = personalId;
			SapId = sapId;
		}

		public IEnumerable<BusinessTrip> BusinessTrips { get; }

		public IEnumerable<Timesheet> Timesheets { get; }

		public Voucher CalculateVouchers(int workingDaysThisMonth, IEnumerable<Operation> outOfOfficeOperations)
			=> new Voucher
			(
				this,
				workingDaysThisMonth
				- Timesheets.OutOfOfficeCount(outOfOfficeOperations)
				- BusinessTrips.Sum(x => x.Days)
			);
	}

	internal static class TimesheetEnumerableExtensions
	{
		public static int OutOfOfficeCount(this IEnumerable<Timesheet> timesheets, IEnumerable<Operation> outOfOfficeOperations)
		{
			return timesheets.Count(x => outOfOfficeOperations.Contains(x.Operation));
		}
	}
}