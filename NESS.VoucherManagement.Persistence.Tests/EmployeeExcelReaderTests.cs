using System;
using System.Linq;

namespace NESS.VoucherManagement.Persistence.Tests
{
	using System.Collections.Generic;

	using Core.Model;

	using Model;

	using Moq;

	using Xunit;

	public class EmployeeExcelReaderTests
	{
		[Fact(DisplayName = "Excel business trips are aggregated for the same employee")]
		public void Test1()
		{
			var contextMock = new Mock<IReadContext>();
			contextMock.Setup(x => x.Employees).Returns(new[]
			{
				new ExcelEmployee
				{
					SapId = "S01"
				}
			});
			contextMock.Setup(x => x.BusinessTrips).Returns(new[]
			{
				new ExcelBusinessTrip
				{
					EmployeeSapId = "S01",
					DaysInBusinessTrip = 13
				},
				new ExcelBusinessTrip
				{
					EmployeeSapId = "S01",
					DaysInBusinessTrip = 10
				}
			});

			var sut = new EmployeeExcelReader(contextMock.Object);

			var result = sut.Employees();

			Assert.Equal(new[]
			{
				new BusinessTrip(13), new BusinessTrip(10)
			}, result.Single().BusinessTrips, new BusinessTripComparer());
		}

		[Fact(DisplayName = "Excel business trip are aggregated per employee")]
		public void Test2()
		{
			var contextMock = new Mock<IReadContext>();
			contextMock.Setup(x => x.Employees).Returns(new[]
			{
				new ExcelEmployee
				{
					SapId = "S01"
				}
			});
			contextMock.Setup(x => x.BusinessTrips).Returns(new[]
			{
				new ExcelBusinessTrip
				{
					EmployeeSapId = "S01",
					DaysInBusinessTrip = 13
				},
				new ExcelBusinessTrip
				{
					EmployeeSapId = "S02",
					DaysInBusinessTrip = 10
				}
			});

			var sut = new EmployeeExcelReader(contextMock.Object);

			var result = sut.Employees();

			Assert.Equal(new[]
			{
				new BusinessTrip(13)
			}, result.Single().BusinessTrips);
		}

		[Fact(DisplayName = "Excel time-sheets are aggregated for the same employee")]
		public void Test3()
		{
			var contextMock = new Mock<IReadContext>();
			contextMock.Setup(x => x.Employees).Returns(new[]
			{
				new ExcelEmployee
				{
					SapId = "S01"
				}
			});
			contextMock.Setup(x => x.TimeSheetEntries).Returns(new[]
			{
				new ExcelTimeSheetEntry
				{
					EmployeeSapId = "S01",
					OperationId = "1",
					OperationDescription = "A"
				},
				new ExcelTimeSheetEntry
				{
					EmployeeSapId = "S01",
					OperationId = "2",
					OperationDescription = "B"
				}
			});

			var sut = new EmployeeExcelReader(contextMock.Object);

			var result = sut.Employees();

			Assert.Equal(new[]
			{
				new TimeSheetEntry(new Operation("1", "A"), DateTime.Now), new TimeSheetEntry(new Operation("2", "B"), DateTime.Now)
			}, result.Single().TimeSheetEntries, new TimesheetComparer());
		}

		[Fact(DisplayName = "Excel time-sheets are aggregated per employee")]
		public void Test4()
		{
			var contextMock = new Mock<IReadContext>();
			contextMock.Setup(x => x.Employees).Returns(new[]
			{
				new ExcelEmployee
				{
					SapId = "S01"
				}
			});
			contextMock.Setup(x => x.TimeSheetEntries).Returns(new[]
			{
				new ExcelTimeSheetEntry()
				{
					EmployeeSapId = "S01",
					OperationId = "1",
					OperationDescription = "A"
				},
				new ExcelTimeSheetEntry()
				{
					EmployeeSapId = "S02",
					OperationId = "2",
					OperationDescription = "B"
				}
			});

			var sut = new EmployeeExcelReader(contextMock.Object);

			var result = sut.Employees();

			Assert.Equal(new[]
			{
				new TimeSheetEntry(new Operation("1", "A"), DateTime.Now), 
			}, result.Single().TimeSheetEntries, new TimesheetComparer());
		}

		[Fact(DisplayName = "No time-sheets for employee")]
		public void Test5()
		{
			var contextMock = new Mock<IReadContext>();
			contextMock.Setup(x => x.Employees).Returns(new[]
			{
				new ExcelEmployee
				{
					SapId = "S01"
				}
			});
			contextMock.Setup(x => x.TimeSheetEntries).Returns(new[]
			{
				new ExcelTimeSheetEntry()
				{
					EmployeeSapId = "S02",
					OperationId = "1",
					OperationDescription = "A"
				},
				new ExcelTimeSheetEntry()
				{
					EmployeeSapId = "S02",
					OperationId = "2",
					OperationDescription = "B"
				}
			});

			var sut = new EmployeeExcelReader(contextMock.Object);

			var result = sut.Employees();

			Assert.Empty(result.Single().TimeSheetEntries);
		}

		[Fact(DisplayName = "No business trips for employee")]
		public void Test6()
		{
			var contextMock = new Mock<IReadContext>();
			contextMock.Setup(x => x.Employees).Returns(new[]
			{
				new ExcelEmployee
				{
					SapId = "S01"
				}
			});
			contextMock.Setup(x => x.BusinessTrips).Returns(new[]
			{
				new ExcelBusinessTrip
				{
					EmployeeSapId = "S02",
					DaysInBusinessTrip = 13
				},
				new ExcelBusinessTrip
				{
					EmployeeSapId = "S02",
					DaysInBusinessTrip = 10
				}
			});

			var sut = new EmployeeExcelReader(contextMock.Object);

			var result = sut.Employees();

			Assert.Empty(result.Single().BusinessTrips);
		}
	}

	public class BusinessTripComparer : IEqualityComparer<BusinessTrip>
	{
		public bool Equals(BusinessTrip x, BusinessTrip y) => x.Days == y.Days;

		public int GetHashCode(BusinessTrip obj) => obj.GetHashCode();
	}

	public class TimesheetComparer : IEqualityComparer<TimeSheetEntry>
	{
		public bool Equals(TimeSheetEntry x, TimeSheetEntry y) => x.Operation.Equals(y.Operation);

		public int GetHashCode(TimeSheetEntry obj) => obj.GetHashCode();
	}
}