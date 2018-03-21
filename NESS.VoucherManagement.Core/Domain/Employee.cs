using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Core.Domain
{
	public class Employee
	{
		//public string FirstName { get; }

		//public string LastName { get; }

		//public string PersonalId { get; }

		//public string SapId { get; }

		private readonly IEnumerable<Timesheet> timesheets;

		private readonly BusinessTrip businessTrip;

		public Employee(IEnumerable<Timesheet> timesheets, BusinessTrip businessTrip)
		{
			//SapId = sapId;
			//PersonalId = personalId;
			//FirstName = firstName;
			//LastName = lastName;
			this.timesheets = timesheets ?? throw new ArgumentNullException(nameof(timesheets));
			this.businessTrip = businessTrip;
		}

		public VoucherInfo CalculateVouchers(int workingDaysThisMonth, IEnumerable<Operation> outOfOfficeOperations)
			=> new VoucherInfo
			(
				this,
				workingDaysThisMonth
				- timesheets.OutOfOfficeCount(outOfOfficeOperations)
				- businessTrip.Days
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