using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using NESS.VoucherManagement.Application;
using NESS.VoucherManagement.Core;

namespace NESS.VoucherManagement.ViewModels
{
	internal class WebServiceHolidayProvider : IHolidayProvider
	{
		public IEnumerable<DateTime> GetHolidays(int year, int month)
		{
			var calendar = new HttpClient().GetStringAsync($"http://kayaposoft.com/enrico/json/v1.0/?action=getPublicHolidaysForYear&year={year}&country=rou").Result;

			return JsonConvert.DeserializeObject<BankHolidayResponseDto[]>(calendar)
			                  .Select(x => new DateTime(x.BankHolidayDateResponseDto.Year, x.BankHolidayDateResponseDto.Month,
				                          x.BankHolidayDateResponseDto.Day))
			                  .Where(x => x.Month == month)
			                  .ToList();
		}
	}
}