namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Core.Model;

	public interface IOutOfOfficeOperationsProvider
	{
		IEnumerable<Operation> GetOutOfOfficeOperations();
	}
}