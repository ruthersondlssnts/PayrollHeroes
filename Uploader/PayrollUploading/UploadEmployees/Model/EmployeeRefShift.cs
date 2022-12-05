using Ganss.Excel;
using System.Collections.Generic;

namespace UploadEmployees.Model
{
    public class EmployeeRefShift
    {
        [Column("Timetable")]
        public string ShiftName { get; set; }
        [Column("On duty")]
        public string ShiftIn { get; set; }
        [Column("Off duty")]
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

        public List<RefShiftDetails> RefShiftDetails { get; set; } = new List<RefShiftDetails> {
                new RefShiftDetails{ Day ="MON",IsRestDay = false, RequiredHour =9 },
                new RefShiftDetails{ Day ="TUE",IsRestDay = false, RequiredHour =9 },
                new RefShiftDetails{ Day ="WED",IsRestDay = false, RequiredHour =9 },
                new RefShiftDetails{ Day ="THU",IsRestDay = false, RequiredHour =9 },
                new RefShiftDetails{ Day ="FRI",IsRestDay = false, RequiredHour =9 },
                new RefShiftDetails{ Day ="SAT",IsRestDay = true, RequiredHour =0 },
                new RefShiftDetails{ Day ="SUN",IsRestDay = true, RequiredHour =0 },
            };

    }
}
