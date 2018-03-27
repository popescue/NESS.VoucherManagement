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

		[Fact(DisplayName = "Inexistent file throws exception")]
		public void Test4()
		{
			var sut = new EmployeeExcelContext("inexistent.file", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx");

			Assert.Throws<FileNotFoundException>(() => sut.Employees.ToList());
		}

		[Fact(DisplayName = "Inexistent file throws exception")]
		public void Test5()
		{
			var sut = new EmployeeExcelContext(@"TestFiles\employees.xlsx", "inexistent.file", @"TestFiles\businessTrips.xlsx");

			Assert.Throws<FileNotFoundException>(() => sut.Timesheets.ToList());
		}

		[Fact(DisplayName = "Inexistent file throws exception")]
		public void Test6()
		{
			var sut = new EmployeeExcelContext(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", "inexistent.file");

			Assert.Throws<FileNotFoundException>(() => sut.BusinessTrips.ToList());
		}

		[Fact(DisplayName = "Not excel file throws exception")]
		public void Test7()
		{
			var sut = new EmployeeExcelContext(@"TestFiles\not_excel.txt", @"TestFiles\timesheets.xlsx", @"TestFiles\businessTrips.xlsx");

			Assert.Throws<InvalidFileTypeException>(() => sut.Employees.ToList());
		}

		[Fact(DisplayName = "Not excel file throws exception")]
		public void Test8()
		{
			var sut = new EmployeeExcelContext(@"TestFiles\employees.xlsx", @"TestFiles\not_excel.txt", @"TestFiles\businessTrips.xlsx");

			Assert.Throws<InvalidFileTypeException>(() => sut.Timesheets.ToList());
		}

		[Fact(DisplayName = "Not excel file throws exception")]
		public void Test9()
		{
			var sut = new EmployeeExcelContext(@"TestFiles\employees.xlsx", @"TestFiles\timesheets.xlsx", @"TestFiles\not_excel.txt");

			Assert.Throws<InvalidFileTypeException>(() => sut.BusinessTrips.ToList());
		}
	}
}