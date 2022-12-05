using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Shared;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("requestDtr")]
    public class RequestDtr : Document
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }
        public string ShiftName { get; set; }
        public string ShiftId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime ShiftDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ActualIn { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ActualOut { get; set; }

        public string Remarks { get; set; }

        public bool IsNightShift { get; set; }
        //Get the current FOR_APPROVAL from Approvers class
        public int ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public string ApproverUserId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverRemarks { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ApproveDate { get; set; }


        public string GroupId { get; set; }
        public string GroupName { get; set; }

        public List<Approvers> Approvers { get; set; }
    }
}
