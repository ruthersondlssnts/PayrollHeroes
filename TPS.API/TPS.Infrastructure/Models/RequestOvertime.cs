using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Shared;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("requestOvertime")]
    public class RequestOvertime : Document
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime OvertimeDate { get; set; }
        public int OvertimeTypeId { get; set; }
        public string OvertimeTypeName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? OvertimeTimeIn { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? OvertimeTimeOut { get; set; }
        public string Remarks { get; set; }

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
