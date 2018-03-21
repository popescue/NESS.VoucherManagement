using System;
using System.Linq;

namespace NESS.VoucherManagement.Persistence.Model
{
    public class ExcelBusinessTrip
    {
        public string EmployeeSapId { get; set; }

        public string CompanyCode { get; set; }

        public string Name { get; set; }

        public int DaysInDelegation { get; set; }
    }
}