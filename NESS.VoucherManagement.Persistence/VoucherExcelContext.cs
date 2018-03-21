using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Persistence.Model;
using Npoi.Mapper;

namespace NESS.VoucherManagement.Persistence
{
	public class VoucherExcelContext
	{
		private readonly string outputFilePath;

		private Mapper vouchersMapper;

		public VoucherExcelContext(string outputFilePath)
		{
			this.outputFilePath = outputFilePath;

			InitializeMappers();
		}

		public IEnumerable<ExcelVoucher> Vouchers { get; set; }

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

		public void SaveChanges()
		{
			vouchersMapper.Save(outputFilePath, Vouchers);
		}
	}
}