namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Application;

	using Core.Model;

	using Model;

	public class VoucherExcelWriter : IVoucherWriter
	{
		private readonly IWriteContext writeContext;

		public VoucherExcelWriter(IWriteContext writeContext) => this.writeContext = writeContext;

		public void WriteVouchers(IEnumerable<Voucher> vouchers)
		{
			writeContext.Vouchers = vouchers
				.OrderBy(v => v.Employee.LastName)
				.ThenBy(v => v.Employee.FirstName)
				.Select(v => new ExcelVoucher
				{
					SapId = v.Employee.SapId,
					FirstName = v.Employee.FirstName,
					LastName = v.Employee.LastName,
					PersonalId = v.Employee.PersonalId,
					Count = v.Count,
					Value = Voucher.Value
				});

			writeContext.SaveChanges();
		}

		public Task WriteVouchersAsync(IEnumerable<Voucher> vouchers)
		{
			writeContext.Vouchers = vouchers
				.OrderBy(v => v.Employee.LastName)
				.ThenBy(v => v.Employee.FirstName)
				.Select(v => new ExcelVoucher
				{
					SapId = v.Employee.SapId,
					FirstName = v.Employee.FirstName,
					LastName = v.Employee.LastName,
					PersonalId = v.Employee.PersonalId,
					Count = v.Count,
					Value = Voucher.Value
				});

			return writeContext.SaveChangesAsync();
		}
	}
}