using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Tests
{
    public class Timesheet
    {
        public Timesheet(Operation operation)
        {
            Operation = operation;
        }

        public Operation Operation { get; }
    }

    public struct Operation
    {
        public string Id { get; set; }

        public string Description { get; set; }
    }
}