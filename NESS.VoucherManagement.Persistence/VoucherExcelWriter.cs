using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Application;
using NESS.VoucherManagement.Core.Model;
using NESS.VoucherManagement.Persistence.Model;

namespace NESS.VoucherManagement.Persistence
{
	public class VoucherExcelWriter : IVoucherWriter
	{
		private readonly VoucherExcelContext writeContext;

		public VoucherExcelWriter(VoucherExcelContext writeContext) => this.writeContext = writeContext;

		public void WriteVouchers(IEnumerable<Voucher> vouchers)
		{
			writeContext.Vouchers = vouchers.Select(v => new ExcelVoucher
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
	}
}