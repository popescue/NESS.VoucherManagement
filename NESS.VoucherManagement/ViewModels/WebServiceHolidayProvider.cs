namespace NESS.VoucherManagement.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using Application;
	using Newtonsoft.Json;

	internal static class WebServiceHolidayProvider
	{
		public static IEnumerable<DateTime> GetHolidays(MonthYear my)
		{
			var calendar = new HttpClient().GetStringAsync($"http://kayaposoft.com/enrico/json/v1.0/?action=getPublicHolidaysForYear&year={my.Year}&country=rou").Result;

			return JsonConvert.DeserializeObject<BankHolidayResponseDto[]>(calendar)
				.Select(x => new DateTime(x.BankHolidayDateResponseDto.Year, x.BankHolidayDateResponseDto.Month,
					x.BankHolidayDateResponseDto.Day))
				.Where(x => x.Month == my.Month)
				.ToList();
		}

		private sealed class BankHolidayResponseDto
		{
			[JsonProperty("date")]
			public BankHolidayDateResponseDto BankHolidayDateResponseDto { get; set; }

			[JsonProperty("localName")]
			public string LocalName { get; set; }

			[JsonProperty("englishName")]
			public string EnglishName { get; set; }
		}

		private sealed class BankHolidayDateResponseDto
		{
			[JsonProperty("day")]
			public int Day { get; set; }

			[JsonProperty("month")]
			public int Month { get; set; }

			[JsonProperty("year")]
			public int Year { get; set; }

			[JsonProperty("dayOfWeek")]
			public int DayOfWeek { get; set; }
		}
	}
}