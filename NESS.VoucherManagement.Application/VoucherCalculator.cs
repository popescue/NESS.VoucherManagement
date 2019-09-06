namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Core.Model;

	public static class VoucherCalculator
	{
		public static IEnumerable<Voucher> CalculateVouchers(IEnumerable<Employee> employees, int daysThisMonth, IEnumerable<Operation> outOfOfficeOperations)
		{
			// ReSharper disable once TooManyChainedReferences
			return employees
				.Select(e => e.CalculateVouchers(daysThisMonth, outOfOfficeOperations))
				.OrderBy(v => v.Employee.LastName)
				.ThenBy(v => v.Employee.FirstName);
		}
	}
}