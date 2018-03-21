﻿using System;
using System.Linq;

namespace NESS.VoucherManagement.Persistence.Model
{
	public class ExcelVoucher
	{
		public decimal Value { get; set; }

		public string SapId { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string PersonalId { get; set; }

		public int Count { get; set; }
	}
}