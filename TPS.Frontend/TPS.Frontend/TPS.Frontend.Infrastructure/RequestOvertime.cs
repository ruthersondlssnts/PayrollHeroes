using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPS.Frontend.Infrastructure
{
    public class RequestOvertime : Document
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        public DateTime OvertimeDate { get; set; }
        public int OvertimeTypeId { get; set; }
        public string OvertimeTypeName { get; set; }
        [DisplayFormat(DataFormatString = "HH:mm:ss")]
        public DateTime? OvertimeTimeIn { get; set; }
        [DisplayFormat(DataFormatString = "HH:mm:ss")]
        public DateTime? OvertimeTimeOut { get; set; }
        public string Remarks { get; set; }

        //Get the current FOR_APPROVAL from Approvers class
        public int ApprovalStatusId { get; set; }
        public string ApprovalStatusName { get; set; }
        public string ApproverUserId { get; set; }
        public string ApproverName { get; set; }
        public string ApproverRemarks { get; set; }
        public DateTime? ApproveDate { get; set; }


        public string GroupId { get; set; }
        public string GroupName { get; set; }

        public List<Approvers> Approvers { get; set; }
    }

}
