using System;
using System.Linq;

namespace NESS.VoucherManagement.Core.Domain
{
	public class Timesheet
	{
		public Timesheet(Operation operation) => Operation = operation;

		public Operation Operation { get; }
	}
}