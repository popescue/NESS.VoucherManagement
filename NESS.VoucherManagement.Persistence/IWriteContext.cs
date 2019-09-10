namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Model;

	public interface IWriteContext
	{
		IEnumerable<ExcelVoucher> Vouchers { get; set; }

		void SaveChanges();
	}
}