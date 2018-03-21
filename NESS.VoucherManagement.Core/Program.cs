using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace NESS.VoucherManagement.Core
{
    public class Program
    {
        private static readonly List<string> DaysOutOfOffice = new List<string> {"1201", "401", "980", "201", "902", "501", "901", "910", "981", "701"};

        public static void Calculate(string employeesXmlPath, string businessTripsXmlPath, string timeKeepingXmlPath, string destinationFolder, int month, int year)
        {
            var timesheets = GetTimesheets(timeKeepingXmlPath);

            var delegations = GetDelegations(businessTripsXmlPath);

            var employees = GetEmployees(employeesXmlPath);

            var vouchers = GetLunchTickets(employees, timesheets, delegations, month, year);

            Export(vouchers, Path.Combine(destinationFolder));
        }

        private static void Export(IEnumerable<ExcelVoucher> tickets, string filePath)
        {
            //var voucherMapper = new Mapper();

            //voucherMapper
            //    .Map<ExcelVoucher>("NUME", x => x.Employee.LastName)
            //    .Map<ExcelVoucher>("PRENUME", x => x.Employee.FirstName)
            //    .Map<ExcelVoucher>("CNP", x => x.Employee.PersonalId)
            //    .Map<ExcelVoucher>("NR_TICHETE", x => x.Count)
            //    .Map<ExcelVoucher>("FV", x => ExcelVoucher.Value).UseFormat(typeof(decimal), "#.00");

            //voucherMapper.Save(filePath, tickets);
        }

        private static IEnumerable<DateTime> GetHolidays(int month, int year)
        {
            var calendar = new HttpClient().GetStringAsync($"http://kayaposoft.com/enrico/json/v1.0/?action=getPublicHolidaysForYear&year={year}&country=rou").Result;

            return JsonConvert.DeserializeObject<BankHolidayResponseDto[]>(calendar)
                .Select(x => new DateTime(x.BankHolidayDateResponseDto.Year, x.BankHolidayDateResponseDto.Month,
                    x.BankHolidayDateResponseDto.Day))
                .Where(x => x.Month == month)
                .ToList();
        }

        private static IEnumerable<ExcelEmployee> GetEmployees(string employeesXmlPath)
        {
            return null;

            //var employeeMapper = new Mapper(employeesXmlPath);

            //employeeMapper
            //    .Map<ExcelEmployee>("Pers.no.", x => x.SapId)
            //    .Map<ExcelEmployee>("Last name", x => x.LastName)
            //    .Map<ExcelEmployee>("First name", x => x.FirstName)
            //    .Map<ExcelEmployee>("ID number", x => x.PersonalId);

            //return employeeMapper.Take<ExcelEmployee>().Select(x => x.Value);
        }

        private static IEnumerable<ExcelBusinessTrip> GetDelegations(string businessTripsXmlPath)
        {
            return null;

            //var delegationMapper = new Mapper(businessTripsXmlPath);

            //delegationMapper
            //    .Map<ExcelBusinessTrip>("Company code", x => x.CompanyCode)
            //    .Map<ExcelBusinessTrip>("SAP ID", x => x.SapId)
            //    .Map<ExcelBusinessTrip>("Nume Prenume", x => x.Name)
            //    .Map<ExcelBusinessTrip>(3, x => x.DaysInDelegation);

            //return delegationMapper.Take<ExcelBusinessTrip>().Select(x => x.Value);
        }

        private static IEnumerable<ExcelTimesheet> GetTimesheets(string timeKeepingXmlPath)
        {
            return null;

            //var timesheetMapper = new Mapper(timeKeepingXmlPath);

            //timesheetMapper
            //    .Map<ExcelTimesheet>("Pers.No.", x => x.EmployeeSapId)
            //    .Map<ExcelTimesheet>("Name", x => x.EmployeeName)
            //    .Map<ExcelTimesheet>("OpAc", x => x.OperationId)
            //    .Map<ExcelTimesheet>("Ac.Descr.", x => x.OperationDescription)
            //    .Map<ExcelTimesheet>("Date", x => x.Date).Format<ExcelTimesheet>("dd.MM.yyyy", t => t.Date);

            //return timesheetMapper.Take<ExcelTimesheet>().Select(x => x.Value);
        }

        private static IEnumerable<ExcelVoucher> GetLunchTickets(IEnumerable<ExcelEmployee> employees,
            IEnumerable<ExcelTimesheet> timesheets, IEnumerable<ExcelBusinessTrip> delegations, int month, int year)
        {
            var workingDaysCount = GetWorkingDaysCount(month, year);

            var bankHolidaysCount = GetHolidays(month, year).Count();

            var tickets = new List<ExcelVoucher>();

            foreach (var employee in employees)
            {
                var timesheet = timesheets.Where(x => x.EmployeeSapId == employee.SapId);

                var daysOff = timesheet.Count(x => DaysOutOfOffice.Contains(x.OperationId));
                var workedDays = timesheet.Count(x => !DaysOutOfOffice.Contains(x.OperationId));

                var delegationDaysOff = delegations.Where(x => x.SapId == employee.SapId).Sum(_ => _.DaysInDelegation);

                var ticketsCount = 0;
                if (workedDays > 0)
                    ticketsCount = workingDaysCount - bankHolidaysCount - daysOff - delegationDaysOff;

                tickets.Add(new ExcelVoucher
                {
                    Employee = employee,
                    Count = ticketsCount
                });
            }

            return tickets;
        }

        private static int GetWorkingDaysCount(int month, int year)
        {
            var daysOfMonth = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(x => new DateTime(year, month, x));

            bool IsWorkingDay(DateTime x) => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday;

            return daysOfMonth.Where(IsWorkingDay).Count();
        }
    }

    public class ExcelVoucher
    {
        public const decimal Value = 15M;

        public ExcelEmployee Employee { get; set; }

        public int Count { get; set; }
    }

    public class ExcelTimesheet
    {
        public string EmployeeSapId { get; set; }

        // Maybe we don't need this column
        public string EmployeeName { get; set; }

        public string OperationId { get; set; }

        public string OperationDescription { get; set; }

        public DateTime Date { get; set; }
    }

    public class ExcelEmployee
    {
        public string SapId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PersonalId { get; set; }
    }

    public class ExcelBusinessTrip
    {
        public string SapId { get; set; }

        public string CompanyCode { get; set; }

        public string Name { get; set; }

        public int DaysInDelegation { get; set; }
    }

    //	internal class CalendarDateConverer : JsonConver
    //	{
    //		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //		{
    //			throw new NotImplementedException();
    //		}
    //
    //		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //		{
    //			throw new NotImplementedException();
    //		}
    //
    //		public override bool CanConvert(Type objectType)
    //		{
    //			throw new NotImplementedException();
    //		}
    //	}
}