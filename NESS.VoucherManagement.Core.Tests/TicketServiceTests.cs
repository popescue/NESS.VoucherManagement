using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NESS.VoucherManagement.Core.Tests
{
    public class TicketServiceTests
    {
        [Fact(DisplayName = "No time-sheets, no tickets")]
        public void Test1()
        {
            // Arrange
            var sut = new TicketService();

            var employee = new EmployeeBuilder().Build();
            var testContext = new TestContextBuilder(employee).Build();

            // Act
            var result = sut.CalculateTickets(testContext.Employee, testContext.WorkingDaysInMonth, testContext.HollidaysInMonth, testContext.OutOfOfficeOperations);

            // Assert
            Assert.Equal(testContext.Employee, result.Employee);
            Assert.Equal(0, result.Count);
        }

        [Fact(DisplayName = "One in office time-sheet, one ticket")]
        public void Test2()
        {
            var sut = new TicketService();

            var provider = new TimesheetProvider();
            var inOfficeTimesheets = provider.InOffice(1);

            var employee = new EmployeeBuilder()
                .WithTimesheets(inOfficeTimesheets)
                .Build();

            var testContext = new TestContextBuilder(employee).Build();

            var result = sut.CalculateTickets(testContext.Employee, testContext.WorkingDaysInMonth, testContext.HollidaysInMonth, testContext.OutOfOfficeOperations);

            Assert.Equal(employee, result.Employee);
            Assert.Equal(1, result.Count);
        }

        [Fact(DisplayName = "Two in office time-sheets, two tickets")]
        public void Test3()
        {
            var sut = new TicketService();

            var provider = new TimesheetProvider();
            var inOfficeTimesheets = provider.InOffice(2);

            var employee = new EmployeeBuilder()
                .WithTimesheets(inOfficeTimesheets)
                .Build();

            var testContext = new TestContextBuilder(employee).Build();

            var result = sut.CalculateTickets(testContext.Employee, testContext.WorkingDaysInMonth, testContext.HollidaysInMonth, testContext.OutOfOfficeOperations);

            Assert.Equal(employee, result.Employee);
            Assert.Equal(2, result.Count);
        }

        [Fact(DisplayName = "One out of office time-sheet, no tickets")]
        public void Test4()
        {
            var sut = new TicketService();

            var outOfOfficeOperations = new[] {new Operation {Description = "Test", Id = "1"}};

            var provider = new TimesheetProvider(outOfOfficeOperations);
            var outOfOfficeTimesheets = provider.OutOfOffice(1);

            var employee = new EmployeeBuilder()
                .WithTimesheets(outOfOfficeTimesheets)
                .Build();

            var testContext = new TestContextBuilder(employee)
                .WithOutOfOfficeOperations(outOfOfficeOperations)
                .Build();

            var result = sut.CalculateTickets(testContext.Employee, testContext.WorkingDaysInMonth, testContext.HollidaysInMonth, testContext.OutOfOfficeOperations);

            Assert.Equal(employee, result.Employee);
            Assert.Equal(0, result.Count);
        }

        [Fact(DisplayName = "One out of office time-sheet and one in office time-sheet, get one ticket")]
        public void Test5()
        {
            var sut = new TicketService();

            var outOfOfficeOperations = new[] {new Operation {Description = "Test", Id = "1"}};

            var provider = new TimesheetProvider(outOfOfficeOperations);
            var outOfOfficeTimesheets = provider.OutOfOffice(1);
            var inOfficeTimesheets = provider.InOffice(1);

            var employee = new EmployeeBuilder()
                .WithTimesheets(outOfOfficeTimesheets.Concat(inOfficeTimesheets))
                .Build();

            var testContext = new TestContextBuilder(employee)
                .WithOutOfOfficeOperations(outOfOfficeOperations)
                .Build();

            var result = sut.CalculateTickets(testContext.Employee, testContext.WorkingDaysInMonth, testContext.HollidaysInMonth, testContext.OutOfOfficeOperations);

            Assert.Equal(employee, result.Employee);
            Assert.Equal(1, result.Count);
        }
    }

    public class TimesheetProvider
    {
        private readonly IEnumerable<Operation> outOfOfficeOperations;

        public TimesheetProvider() => outOfOfficeOperations = Enumerable.Empty<Operation>();

        public TimesheetProvider(IEnumerable<Operation> outOfOfficeOperations) => this.outOfOfficeOperations = outOfOfficeOperations;

        public IEnumerable<Timesheet> OutOfOffice(int count)
        {
            for (var i = 0; i < count; i++)
                yield return new Timesheet
                (
                    new Operation
                    {
                        Id = outOfOfficeOperations.First().Id,
                        Description = outOfOfficeOperations.First().Description
                    }
                );
        }

        public IEnumerable<Timesheet> InOffice(int count)
        {
            var newId = outOfOfficeOperations.Aggregate("0", (id, op) => id + op.Id);
            var newDescription = outOfOfficeOperations.Aggregate("X", (desc, op) => desc + op.Id);

            for (var i = 0; i < count; i++)
                yield return new Timesheet
                (
                    new Operation
                    {
                        Id = newId,
                        Description = newDescription
                    }
                );
        }
    }

    public class TicketService
    {
        public LunchTicket CalculateTickets(Employee employee, int workingDaysInMonth, int hollidaysInMonth, IEnumerable<Operation> outOfOfficeOperations) => new LunchTicket
        {
            Employee = employee,
            Count = employee.Timesheets == null || !employee.Timesheets.Any() ? 0 : employee.Timesheets.Count(x => !outOfOfficeOperations.Contains(x.Operation))
        };
    }
}