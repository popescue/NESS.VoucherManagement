using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Model;

namespace NESS.VoucherManagement.Core.Tests
{
	public class EmployeeBuilder
	{
		private readonly string firstName;

		private readonly string lastName;

		private readonly string personalId;

		private readonly string sapId;

		private IEnumerable<BusinessTrip> businessTrips;

		private IEnumerable<Timesheet> timesheets;

		public EmployeeBuilder()
		{
			businessTrips = Enumerable.Empty<BusinessTrip>();

			firstName = "john";
			lastName = "doe";
			personalId = "1820305035267";
			sapId = "3700804";
			timesheets = Enumerable.Empty<Timesheet>();
		}

		public Employee Build() => new Employee(firstName, lastName, personalId, sapId, timesheets, businessTrips);

		public EmployeeBuilder WithTimesheets(IEnumerable<Timesheet> value)
		{
			timesheets = value;
			return this;
		}

		public EmployeeBuilder WithBusinessTrips(IEnumerable<BusinessTrip> value)
		{
			businessTrips = value;
			return this;
		}
	}
}