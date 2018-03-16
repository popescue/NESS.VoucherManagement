using System;
using System.Collections.Generic;
using System.Linq;

namespace NESS.VoucherManagement.Core.Tests
{
    public class TestContextBuilder
    {
        private readonly Employee employee;

        private readonly int hollidaysInMonth;

        private readonly int workingDaysInMonth;

        private IEnumerable<Operation> outOfOfficeOperations;

        public TestContextBuilder(Employee employee)
        {
            this.employee = employee;

            workingDaysInMonth = 0;
            hollidaysInMonth = 0;
            outOfOfficeOperations = Enumerable.Empty<Operation>();
        }

        public TestContext Build() => new TestContext(employee, workingDaysInMonth, hollidaysInMonth, outOfOfficeOperations);

        public TestContextBuilder WithOutOfOfficeOperations(IEnumerable<Operation> operations)
        {
            outOfOfficeOperations = operations;
            return this;
        }
    }
}