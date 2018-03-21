using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Domain;

namespace NESS.VoucherManagement.Application
{
	public interface IEmployeeReader
	{
		IEnumerable<ExcelEmployee> Read(string path);
	}

	public class EmployeeGetter
	{
		public EmployeeGetter()
		{
			
		}

		public IEnumerable<Employee> GetEmployees(string employeeExcelFilePath, string businessTripExcelFilePath, string timesheetExcelFilePath)
		{
			IEmployeeReader er = new ExcelEmployeeReader();
			IEnumerable<ExcelEmployee> employees = er.Read(EmployeesVm.Path);

			IBusinessTripReader btr = new ExcelBusinessTripReader();
			IEnumerable<ExcelBusinessTrip> businessTrips = btr.Read(BusinessTripsVm.Path);

			ITimekeepingReader tr = new ExcelTimekeepingReader();
			IEnumerable<ExcelTimesheet> timesheets = tr.Read(TimekeepingVm.Path);

			return Enumerable.Empty<Employee>();
		}
	}
}