using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Core.Model;
using Xunit;

namespace NESS.VoucherManagement.Core.Tests
{
    public static class TestResources
    {
        public static readonly IEnumerable<Operation> OutOfOfficeOperations = new[]
        {
            new Operation("1", "Out of Office Operation")
        };
    }

    public class EmployeeTests
    {
        private readonly TimesheetProvider timesheetProvider;

        public EmployeeTests() => timesheetProvider = new TimesheetProvider(TestResources.OutOfOfficeOperations);

        [Theory(DisplayName = "No out-of-office time-sheets and no delegations in the previous month, gets full tickets this month")]
        [InlineData(1, 20, 20)]
        [InlineData(3, 23, 23)]
        public void Test1(int timesheetsInOffice, int workingDaysThisMonth, int expectedTickets)
        {
            // Arrange
            var inOfficeTimesheets = timesheetProvider.InOffice(timesheetsInOffice);

            var employee = new EmployeeBuilder()
                .WithTimesheets(inOfficeTimesheets)
                .Build();

            // Act
            var result = employee.CalculateVouchers(workingDaysThisMonth, TestResources.OutOfOfficeOperations);

            // Assert
            Assert.Equal(expectedTickets, result.Count);
        }

        [Theory(DisplayName = "Subtract out-of-office time-sheets and delegation days in the previous month from current month working days")]
        [InlineData(0, 1, 1, 20, 18)]
        [InlineData(1, 1, 2, 20, 17)]
        [InlineData(3, 3, 5, 20, 12)]
        public void Test2(int timesheetsInOffice, int timesheetsOutOfOffice, int daysInDelegation, int workingDaysThisMonth, int expectedTickets)
        {
            // Arrange
            var inOfficeTimesheets = timesheetProvider.InOffice(timesheetsInOffice);
            var outOfOfficeTimesheets = timesheetProvider.OutOfOffice(timesheetsOutOfOffice);
            var timesheets = inOfficeTimesheets.Concat(outOfOfficeTimesheets);
            var delegation = new[] {new BusinessTrip(daysInDelegation)};

            var employee = new EmployeeBuilder()
                .WithTimesheets(timesheets)
                .WithBusinessTrips(delegation)
                .Build();

            // Act
            var result = employee.CalculateVouchers(workingDaysThisMonth, TestResources.OutOfOfficeOperations);

            // Assert
            Assert.Equal(expectedTickets, result.Count);
        }
    }

    public class TimesheetProvider
    {
        private readonly IEnumerable<Operation> outOfOfficeOperations;

        public TimesheetProvider(IEnumerable<Operation> outOfOfficeOperations) => this.outOfOfficeOperations = outOfOfficeOperations;

        public IEnumerable<Timesheet> OutOfOffice(int count)
        {
            for (var i = 0; i < count; i++)
                yield return new Timesheet
                (
                    new Operation(outOfOfficeOperations.Single().Id, outOfOfficeOperations.Single().Description)
                );
        }

        public IEnumerable<Timesheet> InOffice(int count)
        {
            for (var i = 0; i < count; i++)
                yield return new Timesheet
                (
                    new Operation("100", "In-Office Operation")
                );
        }
    }
}