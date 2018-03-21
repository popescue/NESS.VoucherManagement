using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Persistence.Model;

namespace NESS.VoucherManagement.Persistence
{
    public interface IExcelContext
    {
        IEnumerable<ExcelEmployee> Employees { get; }

        IEnumerable<ExcelBusinessTrip> BusinessTrips { get; }

        IEnumerable<ExcelTimesheet> Timesheets { get; }
    }
}