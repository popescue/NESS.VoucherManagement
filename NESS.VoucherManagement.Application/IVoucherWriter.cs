namespace NESS.VoucherManagement.Application
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Core.Model;

	public interface IVoucherWriter
	{
		void WriteVouchers(IEnumerable<Voucher> vouchers);

		Task WriteVouchersAsync(IEnumerable<Voucher> vouchers);
	}
}