using Newtonsoft.Json;

namespace NESS.VoucherManagement.Core
{
    public class BankHolidayResponseDto
    {
        [JsonProperty("date")]
        public BankHolidayDateResponseDto BankHolidayDateResponseDto { get; set; }

        [JsonProperty("localName")]
        public string LocalName { get; set; }

        [JsonProperty("englishName")]
        public string EnglishName { get; set; }
    }

    public class BankHolidayDateResponseDto
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