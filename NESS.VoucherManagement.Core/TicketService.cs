using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Domain;

namespace NESS.VoucherManagement.Core
{
	public class TicketService
	{
		public LunchTicketInfo CalculateTickets(Employee employee, int workingDays, IEnumerable<Operation> outOfOfficeOperations)
			=> new LunchTicketInfo
			(
				employee,
				workingDays
				- employee.OutOfOfficeDays(outOfOfficeOperations)
				- employee.DelegationDays
			);
	}
}