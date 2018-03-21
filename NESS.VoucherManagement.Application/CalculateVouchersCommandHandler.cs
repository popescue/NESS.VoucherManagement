using System;
using System.Linq;

namespace NESS.VoucherManagement.Application
{
	public class CalculateVouchersCommandHandler
	{
		private readonly IEmployeeReader reader;
		private readonly IVoucherWriter writer;
		private readonly IWorkingDayProvider workingDayProvider;

		public CalculateVouchersCommandHandler(IEmployeeReader reader, IVoucherWriter writer, IWorkingDayProvider workingDayProvider)
		{
			this.reader = reader;
			this.writer = writer;
			this.workingDayProvider = workingDayProvider;
		}

		public void Handle(CalculateVouchersCommand command)
		{
			var employees = reader.GetEmployees();

			var workingDaysThisMonth = workingDayProvider.Count(command.Year, command.Month);

			var vouchers = employees
				.Select(e => e.CalculateVouchers(workingDaysThisMonth, command.OutOfOfficeOperations))
				.OrderBy(v => v.Employee.LastName)
				.ThenBy(v => v.Employee.FirstName);

			writer.WriteVouchers(vouchers);
		}
	}
}