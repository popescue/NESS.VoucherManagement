﻿namespace NESS.VoucherManagement.Hollidays
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;

	using Application;

	using Calendar;

	using Newtonsoft.Json;

	internal class WebServiceHolidayProvider : IHolidayProvider
	{
		public IEnumerable<DateTime> GetHolidays(When when)
		{
			string calendar;

			using (var httpClient = new HttpClient())
			{
				calendar = httpClient.GetStringAsync(CalendarUri(when.Year)).Result;
			}

			var bankHolidays = JsonConvert.DeserializeObject<BankHolidayResponseDto[]>(calendar);

			return GetHolidaysForMonth(bankHolidays, when.Month).ToList();
		}

		public async Task<IEnumerable<DateTime>> GetHolidaysAsync(When when)
		{
			using (var httpClient = new HttpClient())
			{
				var calendar = await httpClient.GetStringAsync(CalendarUri(when.Year)).ConfigureAwait(false);

				var bankHolidays = JsonConvert.DeserializeObject<BankHolidayResponseDto[]>(calendar);

				return GetHolidaysForMonth(bankHolidays, when.Month).ToList();
			}
		}

		private static IEnumerable<DateTime> GetHolidaysForMonth(IEnumerable<BankHolidayResponseDto> bankHolidays, int month)
		{
			return bankHolidays
				.Where(x => x.BankHolidayDateResponseDto.Month == month)
				.Select(x => new DateTime(x.BankHolidayDateResponseDto.Year, x.BankHolidayDateResponseDto.Month,
					x.BankHolidayDateResponseDto.Day));
		}

		private static string CalendarUri(int year)
			=> $"http://kayaposoft.com/enrico/json/v1.0/?action=getPublicHolidaysForYear&year={year}&country=rou";

		private sealed class BankHolidayResponseDto
		{
			[JsonProperty("date")] public BankHolidayDateResponseDto BankHolidayDateResponseDto { get; set; }

			[JsonProperty("localName")] public string LocalName { get; set; }

			[JsonProperty("englishName")] public string EnglishName { get; set; }
		}

		private sealed class BankHolidayDateResponseDto
		{
			[JsonProperty("day")] public int Day { get; set; }

			[JsonProperty("month")] public int Month { get; set; }

			[JsonProperty("year")] public int Year { get; set; }

			[JsonProperty("dayOfWeek")] public int DayOfWeek { get; set; }
		}
	}
}