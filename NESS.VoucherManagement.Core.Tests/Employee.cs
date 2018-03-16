using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Core.Tests {
    public class Employee
    {
        public Employee(string sapId, string personalId, string firstName, string lastName, IEnumerable<Timesheet> timesheets, IEnumerable<Delegation> delegations)
        {
            SapId = sapId;
            PersonalId = personalId;
            FirstName = firstName;
            LastName = lastName;
            Timesheets = timesheets;
            Delegations = delegations;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string PersonalId { get; }

        public string SapId { get; }

        public IEnumerable<Timesheet> Timesheets { get; }

        public IEnumerable<Delegation> Delegations { get; }
    }
}