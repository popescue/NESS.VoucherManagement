namespace NESS.VoucherManagement.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Model;

	using Npoi.Mapper;

	public class VoucherExcelContext : IWriteContext
	{
		private readonly string outputFilePath;

		private Mapper vouchersMapper;

		public VoucherExcelContext(string outputFilePath)
		{
			this.outputFilePath = outputFilePath;

			InitializeMappers();
		}

		public IEnumerable<ExcelVoucher> Vouchers { get; set; }

		public void SaveChanges()
		{
			vouchersMapper.Save(outputFilePath, Vouchers);
		}

		public Task SaveChangesAsync()
		{
			return Task.Run(() => vouchersMapper.Save(outputFilePath, Vouchers));
		}

		// ReSharper disable once TooManyDeclarations
		private void InitializeMappers()
		{
			vouchersMapper = new Mapper()
				.Map<ExcelVoucher>(0, x => x.SapId, "SAP_ID")
				.Map<ExcelVoucher>(1, x => x.LastName, "NUME")
				.Map<ExcelVoucher>(2, x => x.FirstName, "PRENUME")
				.Map<ExcelVoucher>(3, x => x.PersonalId, "CNP")
				.Map<ExcelVoucher>(4, x => x.Count, "NR_TICHETE")
				.Map<ExcelVoucher>(5, x => x.Value, "FV");
		}
	}
}