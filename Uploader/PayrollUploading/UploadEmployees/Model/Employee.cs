using Ganss.Excel;
using System;

namespace UploadEmployees.Model
{
    [BsonCollection("employee")]
    public class Employee : Document
    {
        public string EmployeeSerial { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        [Column("Name")]
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public string ContactNumber { get; set; }
        public string Sss { get; set; }
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
        public EmployeeRefShift RefShift { get; set; } = new EmployeeRefShift();

        //Employee Position/Rank
        public string Grade { get; set; }
        public string Position { get; set; }
        public string Roles { get; set; }
    }
}
