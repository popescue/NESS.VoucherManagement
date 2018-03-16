using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Tests {
    public class LunchTicket
    {
        public const decimal Value = 15M;

        public Employee Employee { get; set; }

        public int Count { get; set; }
    }
}