namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Core.Model;

	public interface IEmployeeReader
	{
		IEnumerable<Employee> GetEmployees();

		Task<IEnumerable<Employee>> GetEmployeesAsync();
	}
}