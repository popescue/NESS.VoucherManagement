using System;
using System.IO;
using System.Linq;
using Xunit;

namespace NESS.VoucherManagement.Persistence.Tests
{
	public class EmployeeExcelContextTests
	{
		[Theory(DisplayName = "Null argument file path throws exception")]
		[InlineData(null, @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx", "employeesFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", null, @"TestFiles\businessTrips.xlsx", "timesheetsFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", null, "businessTripsFilePath")]
		public void Test2(string employeesFilePath, string timesheetsFilePath, string businessTripsFilePath, string nullArgumentName)
		{
			Assert.Throws<ArgumentNullException>(nullArgumentName, () => new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath));
		}

		[Theory(DisplayName = "Whitespace argument file path throws exception")]
		[InlineData("  \t\r\n", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx", "employeesFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", "  \t\r\n", @"TestFiles\businessTrips.xlsx", "timesheetsFilePath")]
		[InlineData(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", "  \t\r\n", "businessTripsFilePath")]
		public void Test3(string employeesFilePath, string timesheetsFilePath, string businessTripsFilePath, string whitespaceArgumentName)
		{
			var ex = Assert.Throws<ArgumentException>(whitespaceArgumentName, () => new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath));

			Assert.Equal("Value must not be empty.\r\nParameter name: " + whitespaceArgumentName, ex.Message);
		}

		[Theory(DisplayName = "File not found throws exception")]
		[InlineData("inexistent.file", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx")]
		[InlineData(@"TestFiles\employees.xlsx", "inexistent.file", @"TestFiles\businessTrips.xlsx")]
		[InlineData(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", "inexistent.file")]
		public void Test4(string employeesFilePath, string timesheetsFilePath, string businessTripsFilePath)
		{
			Assert.Throws<FileNotFoundException>(() => new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath));
		}

		[Theory(DisplayName = "Not excel file throws exception")]
		[InlineData(@"TestFiles\not_excel.txt", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx")]
		[InlineData(@"TestFiles\employees.xlsx", @"TestFiles\not_excel.txt", @"TestFiles\businessTrips.xlsx")]
		[InlineData(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", @"TestFiles\not_excel.txt")]
		public void Test5(string employeesFilePath, string timesheetsFilePath, string businessTripsFilePath)
		{
			var ex = Assert.Throws<InvalidFileTypeException>(() => new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath));

			Assert.Equal(@"TestFiles\not_excel.txt", ex.FilePath);
		}

		[Fact(DisplayName = "Context maps excel files to classes")]
		public void Test1()
		{
			var employeesFilePath = @"TestFiles\employees.xlsx";
			var timesheetsFilePath = @"TestFiles\timesheets.xlsx";
			var businessTripsFilePath = @"TestFiles\businessTrips.xlsx";

			var sut = new EmployeeExcelContext(employeesFilePath, timesheetsFilePath, businessTripsFilePath);

			var employees = sut.Employees.ToList();
			Assert.Equal(4, employees.Count);

			var timesheets = sut.Timesheets.ToList();
			Assert.Equal(20, timesheets.Count);

			var businessTrips = sut.BusinessTrips.ToList();
			Assert.Equal(15, businessTrips.Count);
		}
	}
}