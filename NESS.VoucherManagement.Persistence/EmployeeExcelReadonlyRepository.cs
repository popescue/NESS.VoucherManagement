using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Application;
using NESS.VoucherManagement.Core.Model;

namespace NESS.VoucherManagement.Persistence
{
	public class EmployeeExcelReadonlyRepository : IReadOnlyRepository
	{
		private readonly IExcelContext context;

		public EmployeeExcelReadonlyRepository(IExcelContext context) => this.context = context;

		public IEnumerable<Employee> GetEmployees()
		{
			var employees = from e in context.Employees
			                let businessTrips = context.BusinessTrips.Where(bt => bt.EmployeeSapId == e.SapId).Select(bt => new BusinessTrip(bt.DaysInDelegation))
			                let timesheets = context.Timesheets.Where(t => t.EmployeeSapId == e.SapId).Select(t => new Timesheet(new Operation(t.OperationId, t.OperationDescription)))
			                select new Employee(timesheets, businessTrips);

			return employees;
		}
	}
}