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

		private readonly Delegation delegation;

		public Employee(IEnumerable<Timesheet> timesheets, Delegation delegation)
		{
			//SapId = sapId;
			//PersonalId = personalId;
			//FirstName = firstName;
			//LastName = lastName;
			this.timesheets = timesheets ?? throw new ArgumentNullException(nameof(timesheets));
			this.delegation = delegation;
		}

		public LunchTicketInfo CalculateTickets(int workingDaysThisMonth, IEnumerable<Operation> outOfOfficeOperations)
			=> new LunchTicketInfo
			(
				workingDaysThisMonth
				- timesheets.OutOfOfficeCount(outOfOfficeOperations)
				- delegation.DaysInDelegation
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