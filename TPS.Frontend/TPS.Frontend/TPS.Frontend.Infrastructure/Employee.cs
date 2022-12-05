using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TPS.Frontend.Infrastructure
{
    public class Employee : Document
    {
        public string EmployeeSerial { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public string ContactNumber { get; set; }
        public string Sss { get; set; }
        public string TaxNumber { get; set; }
        public string Pagibig { get; set; }
        public string Philhealth { get; set; }
        public string MaritalStatusId { get; set; }
        public string MaritalStatusName { get; set; }
        public DateTime DateHire { get; set; }
        public DateTime DateRegular { get; set; }
        public DateTime? DateResign { get; set; }
        public string PayTypeId { get; set; }
        public string PayTypeName { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public decimal BasicPay { get; set; }
        public string ElectronicId { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile Photo { get; set; }
        public EmployeeRefShift RefShift { get; set; }

        //Employee Position/Rank
        public string Grade { get; set; }
        public string Position { get; set; }
        public string Roles { get; set; }
    }

    public class EmployeeRefShift
    {
        public string ShiftName { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }
        public string BreakIn { get; set; }
        public string BreakOut { get; set; }
        public decimal BreakHour { get; set; }

        public string NDStart { get; set; }
        public string NDEnd { get; set; }
        public string NDStart2 { get; set; }
        public string NDEnd2 { get; set; }
        public decimal GracePeriod { get; set; }
        public bool IncludeGracePeriod { get; set; }
        public bool IsAutoOvertime { get; set; }
        public bool IsNightDiff { get; set; }

        public List<RefShiftDetails> RefShiftDetails { get; set; }
    }
}
