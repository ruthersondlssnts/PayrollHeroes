using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Enums;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refDayType")]
    public class RefDayType : Document
    {
        public int DayTypeId { get; set; }
        public string DayTypeName { get; set; }
        public decimal OtMultiplier { get; set; }
        public decimal Ot8Multiplier { get; set; }
        public decimal NightDiffMultiplier1 { get; set; }
        public decimal NightDiffMultiplier2 { get; set; }

    }
}
