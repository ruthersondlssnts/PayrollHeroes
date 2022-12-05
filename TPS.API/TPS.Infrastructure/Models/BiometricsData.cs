using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("biometricsData")]
    public class BiometricsData : Document
    {
        public string ElectronicId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime LogDate { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
    }
}
