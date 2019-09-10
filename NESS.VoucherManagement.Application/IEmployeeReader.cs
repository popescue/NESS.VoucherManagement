namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core.Model;

	public interface IEmployeeReader
	{
		IEnumerable<Employee> Employees();
	}
}