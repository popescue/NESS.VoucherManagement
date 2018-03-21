using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core;
using NESS.VoucherManagement.ViewModels;

namespace NESS.VoucherManagement.Persistence
{
	public class ExcelEmployeeReader : IEmployeeReader
	{
		IEnumerable<ExcelEmployee> IEmployeeReader.Read(string path) => throw new NotImplementedException();
	}
}