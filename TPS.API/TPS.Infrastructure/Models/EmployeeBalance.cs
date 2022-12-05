using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Enums;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("employeeBalance")]
    public class EmployeeBalance : Document
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime AcquireDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime ExpireDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal ApprovedQuantity { get; set; }
        public string Remarks { get; set; }
    }
}
