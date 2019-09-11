namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Model;

	public interface IWriteContext
	{
		IEnumerable<ExcelVoucher> Vouchers { get; set; }

		void SaveChanges();

		Task SaveChangesAsync();
	}
}