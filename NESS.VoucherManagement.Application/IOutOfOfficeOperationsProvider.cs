namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IOutOfOfficeOperationsProvider
	{
		IEnumerable<string> GetOperations();
	}
}