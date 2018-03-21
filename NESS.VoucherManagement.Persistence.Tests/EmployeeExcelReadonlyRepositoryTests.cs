using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NESS.VoucherManagement.Core.Model;
using NESS.VoucherManagement.Persistence.Model;
using Xunit;

namespace NESS.VoucherManagement.Persistence.Tests
{
    public class EmployeeExcelReadonlyRepositoryTests
    {
        [Fact(DisplayName = "Excel business trips are aggregated for the same employee")]
        public void Test1()
        {
            var contextMock = new Mock<IExcelContext>();
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
                    DaysInDelegation = 13
                },
                new ExcelBusinessTrip
                {
                    EmployeeSapId = "S01",
                    DaysInDelegation = 10
                }
            });

            var sut = new EmployeeExcelReadonlyRepository(contextMock.Object);

            var result = sut.GetEmployees();

            Assert.Equal(new[] {new BusinessTrip(13), new BusinessTrip(10)}, result.Single().BusinessTrips, new BusinessTripComparer());
        }

        [Fact(DisplayName = "Excel business trip days are aggregated per employee")]
        public void Test2()
        {
            var contextMock = new Mock<IExcelContext>();
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
                    DaysInDelegation = 13
                },
                new ExcelBusinessTrip
                {
                    EmployeeSapId = "S02",
                    DaysInDelegation = 10
                }
            });

            var sut = new EmployeeExcelReadonlyRepository(contextMock.Object);

            var result = sut.GetEmployees();

            Assert.Equal(new[] {new BusinessTrip(13)}, result.Single().BusinessTrips);
        }

        [Fact(DisplayName = "Excel time-sheets are aggregated for the same employee")]
        public void Test3()
        {
            var contextMock = new Mock<IExcelContext>();
            contextMock.Setup(x => x.Employees).Returns(new[]
            {
                new ExcelEmployee
                {
                    SapId = "S01"
                }
            });
            contextMock.Setup(x => x.Timesheets).Returns(new[]
            {
                new ExcelTimesheet
                {
                    EmployeeSapId = "S01",
                    OperationId = "1",
                    OperationDescription = "A"
                },
                new ExcelTimesheet
                {
                    EmployeeSapId = "S01",
                    OperationId = "2",
                    OperationDescription = "B"
                }
            });

            var sut = new EmployeeExcelReadonlyRepository(contextMock.Object);

            var result = sut.GetEmployees();

            Assert.Equal(new[] {new Timesheet(new Operation("1", "A")), new Timesheet(new Operation("2", "B"))}, result.Single().Timesheets, new TimesheetComparer());
        }

        [Fact(DisplayName = "Excel time-sheets are aggregated per employee")]
        public void Test4()
        {
            var contextMock = new Mock<IExcelContext>();
            contextMock.Setup(x => x.Employees).Returns(new[]
            {
                new ExcelEmployee
                {
                    SapId = "S01"
                }
            });
            contextMock.Setup(x => x.Timesheets).Returns(new[]
            {
                new ExcelTimesheet
                {
                    EmployeeSapId = "S01",
                    OperationId = "1",
                    OperationDescription = "A"
                },
                new ExcelTimesheet
                {
                    EmployeeSapId = "S02",
                    OperationId = "2",
                    OperationDescription = "B"
                }
            });

            var sut = new EmployeeExcelReadonlyRepository(contextMock.Object);

            var result = sut.GetEmployees();

            Assert.Equal(new[] {new Timesheet(new Operation("1", "A"))}, result.Single().Timesheets, new TimesheetComparer());
        }
    }

    public class BusinessTripComparer : IEqualityComparer<BusinessTrip>
    {
        public bool Equals(BusinessTrip x, BusinessTrip y) => x.Days == y.Days;

        public int GetHashCode(BusinessTrip obj) => obj.GetHashCode();
    }

    public class TimesheetComparer : IEqualityComparer<Timesheet>
    {
        public bool Equals(Timesheet x, Timesheet y) => x.Operation.Equals(y.Operation);

        public int GetHashCode(Timesheet obj) => obj.GetHashCode();
    }
}