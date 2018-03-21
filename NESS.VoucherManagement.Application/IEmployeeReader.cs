using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Model;

namespace NESS.VoucherManagement.Application
{
	public interface IEmployeeReader
	{
		IEnumerable<Employee> GetEmployees();
	}
}