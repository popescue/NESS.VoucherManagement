using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Core.Tests
{
    public class TestContext
    {
        public TestContext(Employee employee, int workingDaysInMonth, int hollidaysInMonth, IEnumerable<Operation> outOfOfficeOperations)
        {
            Employee = employee;
            WorkingDaysInMonth = workingDaysInMonth;
            HollidaysInMonth = hollidaysInMonth;
            OutOfOfficeOperations = outOfOfficeOperations;
        }

        public Employee Employee { get; }

        public int WorkingDaysInMonth { get; }

        public int HollidaysInMonth { get; }

        public IEnumerable<Operation> OutOfOfficeOperations { get; }
    }
}