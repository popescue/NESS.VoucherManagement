namespace NESS.VoucherManagement.Core.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Model;

	public class EmployeeVoucherCalculator
	{
		public IEnumerable<Voucher> CalculateVouchers(IEnumerable<Employee> employees, int workingDays, IEnumerable<Operation> outOfOfficeOperations)
			=> employees.Select(e => e.CalculateVouchers(workingDays, outOfOfficeOperations));
	}
}