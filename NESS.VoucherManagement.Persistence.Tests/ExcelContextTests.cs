using System;
using System.Linq;
using NESS.VoucherManagement.Persistence.Model;
using Xunit;

namespace NESS.VoucherManagement.Persistence.Tests
{
	public class ExcelContextTests
	{
		[Fact]
		public void Test1()
		{
			var expectedEmployees = new[]
			                        {
				                        new ExcelEmployee
				                        {
					                        SapId = "3700204",
					                        PersonalId = "1810203115230",
					                        FirstName = "Rami-Cristian",
					                        LastName = "Atieyeh"
				                        },
				                        new ExcelEmployee
				                        {
					                        SapId = "3700125",
					                        PersonalId = "2810203115230",
					                        FirstName = "Nadia-Simona",
					                        LastName = "Ocnariu"
				                        },
				                        new ExcelEmployee
				                        {
					                        SapId = "3700105",
					                        PersonalId = "1820203115230",
					                        FirstName = "Eduard",
					                        LastName = "Popescu"
				                        },
				                        new ExcelEmployee
				                        {
					                        SapId = "3700811",
					                        PersonalId = "1830203115230",
					                        FirstName = "Gheorghe",
					                        LastName = "Constantin"
				                        }
			                        };

			var employeesFilePath = @"ExcelFiles\employees.xlsx";
			var timekeepingFilePath = @"ExcelFiles\timesheets.xlsx";
			var businessTripsFilePath = @"ExcelFiles\businessTrips.xlsx";

			var sut = new EmployeeExcelContext(employeesFilePath, timekeepingFilePath, businessTripsFilePath);

			var employees = sut.Employees.ToList();

			Assert.Equal(4, employees.Count);
			Assert.Equal(expectedEmployees, employees, ExcelEmployee.ExcelEmployeeComparer);
		}
	}
}