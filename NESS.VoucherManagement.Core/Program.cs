﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Npoi.Mapper;

namespace NESS.VoucherManagement.Core
{
    public class Program
    {
        private static readonly List<string> DaysOutOfOffice = new List<string> {"1201", "401", "980", "201", "902", "501", "901", "910", "981", "701" };


        public static void Calculate(string employeesXmlPath, string businessTripsXmlPath, string timeKeepingXmlPath, string destinationFolder, int v, int selectedYear)
        {

            var timesheets = GetTimesheets(timeKeepingXmlPath);

            var delegations = GetDelegations(businessTripsXmlPath);

            var employees = GetEmployees(employeesXmlPath);

            var tickets = GetLunchTickets(employees, timesheets, delegations, v, selectedYear);

            Export(tickets, Path.Combine(destinationFolder));
        }

        private static void Export(IEnumerable<LunchTicket> tickets, string filePath)
        {
            var lunchTicketMapper = new Mapper();

            lunchTicketMapper
                .Map<LunchTicket>("NUME", x => x.LastName)
                .Map<LunchTicket>("PRENUME", x => x.FirstName)
                .Map<LunchTicket>("CNP", x => x.PersonalId)
                .Map<LunchTicket>("NR_TICHETE", x => x.Count)
                .Map<LunchTicket>("FV", x => x.Value).UseFormat(typeof(decimal), "#.00");

            lunchTicketMapper.Save(filePath, tickets);

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

        private static IEnumerable<Employee> GetEmployees(string employeesXmlPath)
        {
            var employeeMapper = new Mapper(employeesXmlPath);

            employeeMapper
                .Map<Employee>("Pers.no.", x => x.SapId)
                .Map<Employee>("Last name", x => x.LastName)
                .Map<Employee>("First name", x => x.FirstName)
                .Map<Employee>("ID number", x => x.PersonalId);

            return employeeMapper.Take<Employee>().Select(x => x.Value);
        }

        private static IEnumerable<Delegation> GetDelegations(string businessTripsXmlPath)
        {
            var delegationMapper = new Mapper(businessTripsXmlPath);

            delegationMapper
                .Map<Delegation>("Company code", x => x.CompanyCode)
                .Map<Delegation>("SAP ID", x => x.SapId)
                .Map<Delegation>("Nume Prenume", x => x.Name)
                .Map<Delegation>(3, x => x.DaysInDelegation);

            return delegationMapper.Take<Delegation>().Select(x => x.Value);
        }

        private static IEnumerable<Timesheet> GetTimesheets(string timeKeepingXmlPath)
        {
            var timesheetMapper = new Mapper(timeKeepingXmlPath);

            timesheetMapper
                .Map<Timesheet>("Pers.No.", x => x.SapId)
                .Map<Timesheet>("Name", x => x.Name)
                .Map<Timesheet>("OpAc", x => x.OperationId)
                .Map<Timesheet>("Ac.Descr.", x => x.OperationDescription)
                .Map<Timesheet>("Date", x => x.Date).Format<Timesheet>("dd.MM.yyyy", t=>t.Date);

            return timesheetMapper.Take<Timesheet>().Select(x => x.Value);
        }

        private static IEnumerable<LunchTicket> GetLunchTickets(IEnumerable<Employee> employees,
            IEnumerable<Timesheet> timesheets, IEnumerable<Delegation> delegations, int month, int year)
        {
            var workingDaysCount = GetWorkingDaysCount(month, year);

            var bankHolidaysCount = GetHolidays(month, year).Count();

            var tickets = new List<LunchTicket>();

            foreach (var employee in employees)
            {
                var timesheet = timesheets.Where(x => x.SapId == employee.SapId);

                var daysOff = timesheet.Count(x => DaysOutOfOffice.Contains(x.OperationId));
                var workedDays = timesheet.Count(x => !DaysOutOfOffice.Contains(x.OperationId));

                var delegationDaysOff = delegations.Where(x => x.SapId == employee.SapId).Sum(_ =>_.DaysInDelegation);

                var ticketsCount = 0;
                if (workedDays > 0)
                    ticketsCount = workingDaysCount - bankHolidaysCount - daysOff - delegationDaysOff;
                
                tickets.Add(new LunchTicket
                {
                    PersonalId = employee.PersonalId,
                    LastName= employee.LastName,
                    FirstName= employee.FirstName,
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

    internal class LunchTicket
    {
//        [Column("NUME")]
        public string LastName { get; set; }

//        [Column("PRENUME")]
        public string FirstName { get; set; }

//        [Column("CNP")]
        public string PersonalId { get; set; }
        
//        [Column("NR_TICHETE")]
        public int Count { get; set; }

//        [Column("FV")]
        public decimal Value => 15M;
    }

    internal class Timesheet
    {
        public string SapId { get; set; }

        public string Name { get; set; }

        public string OperationId { get; set; }

        public string OperationDescription { get; set; }

        public DateTime Date { get; set; }
    }

    internal class Employee
    {
        public string SapId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PersonalId { get; set; }
    }

    internal class Delegation
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