namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Application;

	using Core.Model;

	public class EmployeeExcelReader : IEmployeeReader
	{
		private readonly IContext context;

		public EmployeeExcelReader(IContext context) => this.context = context;

		public IEnumerable<Employee> Employees()
		{
			var excelEmployees = context.GetEmployees().ToList();
			var excelBusinessTrips = context.GetBusinessTrips().ToList();
			var excelTimeSheetEntries = context.GetTimeSheetEntries().ToList();

			var employees = from e in excelEmployees
				let businessTrips = excelBusinessTrips
					.Where(bt => bt.EmployeeSapId == e.SapId)
					.Select(bt => new BusinessTrip(bt.DaysInBusinessTrip))
				let timesheets = excelTimeSheetEntries
					.Where(t => t.EmployeeSapId == e.SapId)
					.Select(t => new TimeSheetEntry(new Operation(t.OperationId, t.OperationDescription), t.Date))
				select new Employee(e.FirstName, e.LastName, e.PersonalId, e.SapId, timesheets, businessTrips);

			return employees;
		}
	}
}