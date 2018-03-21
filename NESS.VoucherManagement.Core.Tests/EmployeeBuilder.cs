using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Domain;

namespace NESS.VoucherManagement.Core.Tests
{
	public class EmployeeBuilder
	{
		//private readonly string firstName;

		//private readonly string lastName;

		//private readonly string personalId;

		//private readonly string sapId;

		private BusinessTrip businessTrip;

		private IEnumerable<Timesheet> timesheets;

		public EmployeeBuilder()
		{
			businessTrip = BusinessTrip.NoDelegation();
			//firstName = "john";
			//lastName = "doe";
			//personalId = "1820305035267";
			//sapId = "3700804";
			timesheets = Enumerable.Empty<Timesheet>();
		}

		public Employee Build() => new Employee(timesheets, businessTrip);

		public EmployeeBuilder WithTimesheets(IEnumerable<Timesheet> timesheets1)
		{
			timesheets = timesheets1;
			return this;
		}

		public EmployeeBuilder WithDelegation(BusinessTrip delegation1)
		{
			businessTrip = delegation1;
			return this;
		}
	}
}