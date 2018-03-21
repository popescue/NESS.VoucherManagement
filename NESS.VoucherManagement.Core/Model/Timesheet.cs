using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Model
{
    public struct Timesheet
    {
        public Timesheet(Operation operation) => Operation = operation;

        public Operation Operation { get; }
    }
}