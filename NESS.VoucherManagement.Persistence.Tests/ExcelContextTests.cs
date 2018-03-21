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
				                        }
			                        };

			var employeesFilePath = @"ExcelFiles\employees.xlsx";
			var timekeepingFilePath = @"ExcelFiles\timekeeping.xlsx";
			var businessTripsFilePath = @"ExcelFiles\businessTrips.xlsx";

			var sut = new ExcelContext(employeesFilePath, timekeepingFilePath, businessTripsFilePath);

			var emps = sut.Employees.ToList();

			Assert.Equal(4, emps.Count);
			Assert.Equal(expectedEmployees, emps, ExcelEmployee.ExcelEmployeeComparer);
		}
	}
}