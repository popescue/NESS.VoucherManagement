namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Linq;

	using Core.Services;

	public sealed class VouchersUseCase
	{
		private readonly IEmployeeReader employeeReader;

		private readonly IOutOfOfficeOperationsProvider outOfOfficeOperationsProvider;

		private readonly EmployeeVoucherCalculator voucherCalculator;

		private readonly IVoucherWriter voucherWriter;

		private readonly IWorkingDaysProvider workingDaysProvider;

		public VouchersUseCase(IEmployeeReader employeeReader, IVoucherWriter voucherWriter, IOutOfOfficeOperationsProvider outOfOfficeOperationsProvider, IWorkingDaysProvider workingDaysProvider)
		{
			this.employeeReader = employeeReader;
			this.voucherWriter = voucherWriter;
			this.outOfOfficeOperationsProvider = outOfOfficeOperationsProvider;
			this.workingDaysProvider = workingDaysProvider;

			voucherCalculator = new EmployeeVoucherCalculator();
		}

		public void CalculateVouchers(MonthYear monthYear)
		{
			var employees = employeeReader.Employees();
			var workingDays = workingDaysProvider.WorkingDaysForMonth(monthYear);
			var outOfOfficeOperations = outOfOfficeOperationsProvider.GetOutOfOfficeOperations();

			var vouchers = voucherCalculator.CalculateVouchers(employees, workingDays, outOfOfficeOperations);

			voucherWriter.WriteVouchers(vouchers);
		}
	}
}