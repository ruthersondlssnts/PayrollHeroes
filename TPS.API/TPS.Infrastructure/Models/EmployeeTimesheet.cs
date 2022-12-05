using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("employeeTimesheet")]
    public class EmployeeTimesheet : Document
    {
        public string EmployeeId { get; set; }
        public string PayrollCutoffId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string FullName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime? ShiftDate { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }
        public decimal RequiredHour { get; set; }
        public bool IsNightShift { get; set; }
        public int DayTypeId { get; set; }
        public decimal GracePeriod { get; set; }
        public string DayTypeName { get; set; }

        public int HolidayTypeId { get; set; }
        public string HolidayTypeName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ActualIn { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ActualOut { get; set; }
        public decimal RenderedHour { get; set; }
        public decimal RenderedHourByShift { get; set; }

        //DTR Approved
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DtrIn { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DtrOut { get; set; }
        public decimal DtrRenderedHour { get; set; }

        public decimal DtrRenderedHourByShift { get; set; }

        //OT Approved
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? OvertimeTimeIn { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? OvertimeTimeOut { get; set; }
        public decimal Overtime { get; set; }
        public decimal Overtime8 { get; set; }

        public decimal NightDiff { get; set; }
        public decimal NightDiff8 { get; set; }


        public decimal Absent { get; set; }
        public decimal Late { get; set; }
        public decimal Undertime { get; set; }

        //LEAVE
        public decimal ApproveLeave { get; set; }
        public string LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }

        public bool IsProcessedDTR { get; set; }
        public bool IsProcessedPayroll { get; set; }

        public DateTime? ProcessDate { get; set; }
    }
}
