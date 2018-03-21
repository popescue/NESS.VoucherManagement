using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Core.Model
{
	public class Employee
	{
		//public string FirstName { get; }

		//public string LastName { get; }

		//public string PersonalId { get; }

		//public string SapId { get; }

		public Employee(IEnumerable<Timesheet> timesheets, IEnumerable<BusinessTrip> businessTrips)
		{
			//SapId = sapId;
			//PersonalId = personalId;
			//FirstName = firstName;
			//LastName = lastName;
			Timesheets = timesheets ?? throw new ArgumentNullException(nameof(timesheets));
			BusinessTrips = businessTrips;
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