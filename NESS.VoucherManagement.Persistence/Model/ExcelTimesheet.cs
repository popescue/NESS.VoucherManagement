using System;
using System.Linq;

namespace NESS.VoucherManagement.Persistence.Model
{
    public class ExcelTimesheet
    {
        public string EmployeeSapId { get; set; }

        // Maybe we don't need this column
        public string EmployeeName { get; set; }

        public string OperationId { get; set; }

        public string OperationDescription { get; set; }

        public DateTime Date { get; set; }
    }
}