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
			this.delegation = delegation ?? throw new ArgumentNullException(nameof(delegation));
		}

		public int DelegationDays => delegation.DaysInDelegation;

		public int OutOfOfficeDays(IEnumerable<Operation> outOfOfficeOperations) => timesheets.OutOfOfficeCount(outOfOfficeOperations);
	}
}