using System;
using System.Collections.Generic;
using System.Linq;
using NESS.VoucherManagement.Persistence.Model;
using Npoi.Mapper;

namespace NESS.VoucherManagement.Persistence
{
    public class ExcelContext : IExcelContext
    {
        private readonly string businessTripsFilePath;

        private readonly string employeesFilePath;

        private readonly string timekeepingFilePath;

        private IEnumerable<ExcelBusinessTrip> businessTripSet;

        private Mapper businessTripsMapper;

        private IEnumerable<ExcelEmployee> employeeSet;

        private Mapper employeesMapper;

        private Mapper timesheetMapper;

        private IEnumerable<ExcelTimesheet> timesheetSet;

        public ExcelContext(string employeesFilePath, string timekeepingFilePath, string businessTripsFilePath)
        {
            this.employeesFilePath = employeesFilePath;
            this.timekeepingFilePath = timekeepingFilePath;
            this.businessTripsFilePath = businessTripsFilePath;

            InitializeMappers();
        }

        public IEnumerable<ExcelTimesheet> Timesheets => timesheetSet ?? (timesheetSet = timesheetMapper.Take<ExcelTimesheet>().Select(x => x.Value));

        public IEnumerable<ExcelEmployee> Employees => employeeSet ?? (employeeSet = employeesMapper.Take<ExcelEmployee>().Select(x => x.Value));

        public IEnumerable<ExcelBusinessTrip> BusinessTrips => businessTripSet ?? (businessTripSet = businessTripsMapper.Take<ExcelBusinessTrip>().Select(x => x.Value));

        private void InitializeMappers()
        {
            employeesMapper = new Mapper(employeesFilePath);

            employeesMapper
                .Map<ExcelEmployee>("Pers.no.", x => x.SapId)
                .Map<ExcelEmployee>("Last name", x => x.LastName)
                .Map<ExcelEmployee>("First name", x => x.FirstName)
                .Map<ExcelEmployee>("ID number", x => x.PersonalId);

            businessTripsMapper = new Mapper(businessTripsFilePath);

            businessTripsMapper
                .Map<ExcelBusinessTrip>("Company code", x => x.CompanyCode)
                .Map<ExcelBusinessTrip>("SAP ID", x => x.EmployeeSapId)
                .Map<ExcelBusinessTrip>("Nume Prenume", x => x.Name)
                .Map<ExcelBusinessTrip>(3, x => x.DaysInDelegation);

            timesheetMapper = new Mapper(timekeepingFilePath);

            timesheetMapper
                .Map<ExcelTimesheet>("Pers.No.", x => x.EmployeeSapId)
                .Map<ExcelTimesheet>("Name", x => x.EmployeeName)
                .Map<ExcelTimesheet>("OpAc", x => x.OperationId)
                .Map<ExcelTimesheet>("Ac.Descr.", x => x.OperationDescription)
                .Map<ExcelTimesheet>("Date", x => x.Date).Format<ExcelTimesheet>("dd.MM.yyyy", t => t.Date);
        }
    }
}