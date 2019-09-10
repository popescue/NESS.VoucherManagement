namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Linq;

	using Core.Services;

	public sealed class VouchersUseCase
	{
		private readonly IEmployeeReader employeeReader;

		private readonly IOutOfOfficeOperationsProvider outOfOfficeOperationsProvider;

		private readonly VouchersService vouchersService;

		private readonly IVoucherWriter voucherWriter;

		private readonly IWorkingDaysProvider workingDaysProvider;

		// ReSharper disable once TooManyDependencies
		public VouchersUseCase(IEmployeeReader employeeReader, IVoucherWriter voucherWriter, IOutOfOfficeOperationsProvider outOfOfficeOperationsProvider, IWorkingDaysProvider workingDaysProvider)
		{
			this.employeeReader = employeeReader;
			this.voucherWriter = voucherWriter;
			this.outOfOfficeOperationsProvider = outOfOfficeOperationsProvider;
			this.workingDaysProvider = workingDaysProvider;

			vouchersService = new VouchersService();
		}

		public void CalculateVouchers(When when)
		{
			var employees = employeeReader.GetEmployees();
			var workingDays = workingDaysProvider.GetWorkingDays(when);
			var outOfOfficeOperations = outOfOfficeOperationsProvider.GetOperations();

			var vouchers = vouchersService.CalculateVouchers(employees, workingDays, outOfOfficeOperations);

			voucherWriter.WriteVouchers(vouchers);
		}
	}
}