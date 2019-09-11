using System;
using System.Linq;

namespace NESS.VoucherManagement.Persistence.Tests
{
	using System.IO;

	using Xunit;

	public class EmployeeExcelContextTests
	{
		[Theory(DisplayName = "Null argument file path throws exception")]
		[InlineData(null, @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx", "employeesFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", null, @"TestFiles\businessTrips.xlsx", "timeSheetsFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", null, "businessTripsFilePath")]
		public void Test2(string employeesFilePath, string timesheetsFilePath, string businessTripsFilePath, string nullArgumentName)
		{
			Assert.Throws<ArgumentNullException>(nullArgumentName, () => new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath));
		}

		[Theory(DisplayName = "Whitespace argument file path throws exception")]
		[InlineData("  \t\r\n", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx", "employeesFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", "  \t\r\n", @"TestFiles\businessTrips.xlsx", "timeSheetsFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", "  \t\r\n", "businessTripsFilePath")]
		public void Test3(string employeesFilePath, string timesheetsFilePath, string businessTripsFilePath, string whitespaceArgumentName)
		{
			var ex = Assert.Throws<ArgumentException>(whitespaceArgumentName, () => new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath));

			Assert.Equal("Value must not be empty.\r\nParameter name: " + whitespaceArgumentName, ex.Message);
		}

		[Fact(DisplayName = "Context maps excel files to classes")]
		public void Test1()
		{
			const string employeesFilePath = @"TestFiles\employees.xlsx";
			const string timesheetsFilePath = @"TestFiles\timesheets.xlsx";
			const string businessTripsFilePath = @"TestFiles\businessTrips.xlsx";

			var sut = new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath);

			var employees = sut.Employees.ToList();
			Assert.Equal(4, employees.Count);

			var timeSheets = sut.TimeSheetEntries.ToList();
			Assert.Equal(20, timeSheets.Count);

			var businessTrips = sut.BusinessTrips.ToList();
			Assert.Equal(15, businessTrips.Count);
		}

		[Fact(DisplayName = "Nonexistent file throws exception")]
		public void Test4()
		{
			Assert.Throws<FileNotFoundException>(() => new EmployeeExcelContext("nonexistent.file", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx"));
		}

		[Fact(DisplayName = "Nonexistent file throws exception")]
		public void Test5()
		{
			Assert.Throws<FileNotFoundException>(() => new EmployeeExcelContext(@"TestFiles\employees.xlsx", "nonexistent.file", @"TestFiles\businessTrips.xlsx"));
		}

		[Fact(DisplayName = "Nonexistent file throws exception")]
		public void Test6()
		{
			Assert.Throws<FileNotFoundException>(() => new EmployeeExcelContext(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", "nonexistent.file"));
		}

		[Fact(DisplayName = "Not excel file throws exception")]
		public void Test7()
		{
			Assert.Throws<InvalidFileTypeException>(() => new EmployeeExcelContext(@"TestFiles\not_excel.txt", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx"));
		}

		[Fact(DisplayName = "Not excel file throws exception")]
		public void Test8()
		{
			Assert.Throws<InvalidFileTypeException>(() => new EmployeeExcelContext(@"TestFiles\employees.xlsx", @"TestFiles\not_excel.txt", @"TestFiles\businessTrips.xlsx"));
		}

		[Fact(DisplayName = "Not excel file throws exception")]
		public void Test9()
		{
			Assert.Throws<InvalidFileTypeException>(() => new EmployeeExcelContext(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", @"TestFiles\not_excel.txt"));
		}
	}
}