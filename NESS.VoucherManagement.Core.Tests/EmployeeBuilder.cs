using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Core.Tests {
    public class EmployeeBuilder
    {
        private readonly IEnumerable<Delegation> delegations;

        private readonly string firstName;

        private readonly string lastName;

        private readonly string personalId;

        private readonly string sapId;

        private IEnumerable<Timesheet> timesheets;

        public EmployeeBuilder()
        {
            delegations = Enumerable.Empty<Delegation>();
            firstName = "john";
            lastName = "doe";
            personalId = "1820305035267";
            sapId = "3700804";
            timesheets = Enumerable.Empty<Timesheet>();
        }

        public Employee Build() => new Employee(sapId, personalId, firstName, lastName, timesheets, delegations);

        public EmployeeBuilder WithTimesheets(IEnumerable<Timesheet> timesheets1)
        {
            timesheets = timesheets1;
            return this;
        }
    }
}