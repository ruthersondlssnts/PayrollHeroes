using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refShift")]
    public class RefShift : Document
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

    public class RefShiftDetails 
    {
        public string Day { get; set; }
        public decimal RequiredHour { get; set; }
        public bool IsRestDay { get; set; }
    }
}
