using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refLeaveType")]
    public class RefLeaveType : Document
    {
        public string LeaveTypeName { get; set; }
        public string LeaveTypeCode { get; set; }
        public bool IsWithpay { get; set; }

        //Get from employee balance
        public bool IsCheckBalance { get; set; }
    }
}
