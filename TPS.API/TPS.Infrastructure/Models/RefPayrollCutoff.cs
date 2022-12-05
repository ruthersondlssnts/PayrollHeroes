using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refPayrollCutoff")]
    public class RefPayrollCutoff : Document
    {
        public string Name { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime PayrollDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime CutoffStartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime CutoffEndDate { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsProcessGovernment { get; set; }
        public int CutoffCountPerMonth { get; set; }
    }
}
