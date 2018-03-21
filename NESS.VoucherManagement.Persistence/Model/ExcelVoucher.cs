using System;
using System.Linq;

namespace NESS.VoucherManagement.Persistence.Model
{
    public class ExcelVoucher
    {
        public const decimal Value = 15M;

        public ExcelEmployee Employee { get; set; }

        public int Count { get; set; }
    }
}