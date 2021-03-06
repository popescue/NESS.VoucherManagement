﻿namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Application;

	using Core.Model;

	public class EmployeeExcelReader : IEmployeeReader
	{
		private readonly IReadContext readContext;

		public EmployeeExcelReader(IReadContext readContext) => this.readContext = readContext;

		public IEnumerable<Employee> GetEmployees()
		{
			var excelEmployees = readContext.Employees.ToList();
			var excelBusinessTrips = readContext.BusinessTrips.ToList();
			var excelTimeSheetEntries = readContext.TimeSheetEntries.ToList();

			var employees = from e in excelEmployees
				let businessTrips = excelBusinessTrips
					.Where(bt => bt.EmployeeSapId == e.SapId)
					.Select(bt => new BusinessTrip(bt.DaysInBusinessTrip))
				let timesheets = excelTimeSheetEntries
					.Where(t => t.EmployeeSapId == e.SapId)
					.Select(t => new TimeSheetEntry(t.Operation, t.Date))
				select new Employee(e.FirstName, e.LastName, e.PersonalId, e.SapId, timesheets, businessTrips);

			return employees;
		}

		public async Task<IEnumerable<Employee>> GetEmployeesAsync()
		{
			var excelEmployees = await readContext.GetEmployeesAsync().ConfigureAwait(false);
			var excelBusinessTrips = await readContext.GetBusinessTripsAsync().ConfigureAwait(false);
			var excelTimeSheetEntries = await readContext.GetTimeSheetEntriesAsync().ConfigureAwait(false);

			var employees = from e in excelEmployees
				let businessTrips = excelBusinessTrips
					.Where(bt => bt.EmployeeSapId == e.SapId)
					.Select(bt => new BusinessTrip(bt.DaysInBusinessTrip))
				let timesheets = excelTimeSheetEntries
					.Where(t => t.EmployeeSapId == e.SapId)
					.Select(t => new TimeSheetEntry(t.Operation, t.Date))
				select new Employee(e.FirstName, e.LastName, e.PersonalId, e.SapId, timesheets, businessTrips);

			return employees.ToList();
		}
	}
}