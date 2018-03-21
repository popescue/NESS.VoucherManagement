using System;
using System.Linq;
using Moq;
using NESS.VoucherManagement.Application;
using NESS.VoucherManagement.ViewModels;
using Xunit;

namespace NESS.VoucherManagement.Tests
{
	public class Class1
	{
		[Theory(DisplayName = "Gives correct week days count per month")]
		[InlineData(2018, 1, 23)]
		[InlineData(2018, 2, 20)]
		[InlineData(2018, 3, 22)]
		[InlineData(2018, 4, 21)]
		[InlineData(2018, 5, 23)]
		[InlineData(2018, 6, 21)]
		[InlineData(2018, 7, 22)]
		[InlineData(2018, 8, 23)]
		[InlineData(2018, 9, 20)]
		[InlineData(2018, 10, 23)]
		[InlineData(2018, 11, 22)]
		[InlineData(2018, 12, 21)]
		public void Test1(int year, int month, int expectedWeekDays)
		{
			var holidayProviderMock = new Mock<IHolidayProvider>();

			var sut = new CalendarWorkingDayProvider(holidayProviderMock.Object);

			var result = sut.Count(year, month);

			Assert.Equal(expectedWeekDays, result);
		}

		[Fact(DisplayName = "Excludes holidays during the weekdays")]
		public void Test2()
		{
			var year = 2018;
			var month = 1;

			var holidayProviderMock = new Mock<IHolidayProvider>();
			holidayProviderMock.Setup(x => x.GetHolidays(year, month))
			                   .Returns(new[]
			                            {
				                            new DateTime(year, month, 1),
				                            new DateTime(year, month, 2),
				                            new DateTime(year, month, 24)
			                            });

			var sut = new CalendarWorkingDayProvider(holidayProviderMock.Object);

			var result = sut.Count(year, month);

			Assert.Equal(20, result);
		}

		[Fact(DisplayName = "Ignores holidays during the weekends")]
		public void Test3()
		{
			var year = 2018;
			var month = 4;

			var holidayProviderMock = new Mock<IHolidayProvider>();
			holidayProviderMock.Setup(x => x.GetHolidays(year, month))
			                   .Returns(new[]
			                            {
				                            new DateTime(year, month, 7),
				                            new DateTime(year, month, 8)
			                            });

			var sut = new CalendarWorkingDayProvider(holidayProviderMock.Object);

			var result = sut.Count(year, month);

			Assert.Equal(21, result);
		}
	}
}