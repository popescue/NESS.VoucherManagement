namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public interface IOutOfOfficeOperationsProvider
	{
		IEnumerable<string> GetOperations();

		Task<IEnumerable<string>> GetOperationsAsync();
	}
}