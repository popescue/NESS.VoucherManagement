using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Model;

namespace NESS.VoucherManagement.Application
{
	public class CalculateVouchersCommand
	{
		public CalculateVouchersCommand(IEnumerable<Operation> outOfOfficeOperations, int year, int month)
		{
			OutOfOfficeOperations = outOfOfficeOperations;
			Year = year;
			Month = month;
		}

		public IEnumerable<Operation> OutOfOfficeOperations { get; }
		public int Year { get; }
		public int Month { get; }
	}
}