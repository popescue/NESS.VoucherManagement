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

		public IEnumerable<Employee> GetEmployees()
		{
			var employees = from e in context.GetEmployees()
				let businessTrips = context.GetBusinessTrips()
					.Where(bt => bt.EmployeeSapId == e.SapId)
					.Select(bt => new BusinessTrip(bt.DaysInBusinessTrip))
				let timesheets = context.GetTimeSheetEntries()
					.Where(t => t.EmployeeSapId == e.SapId)
					.Select(t => new TimeSheet(new Operation(t.OperationId, t.OperationDescription), t.Date))
				select new Employee(e.FirstName, e.LastName, e.PersonalId, e.SapId, timesheets, businessTrips);

			return employees;
		}
	}
}