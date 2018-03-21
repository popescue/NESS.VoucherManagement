using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Model;

namespace NESS.VoucherManagement.Application
{
	public interface IReadOnlyRepository
	{
		IEnumerable<Employee> GetEmployees();
	}
}