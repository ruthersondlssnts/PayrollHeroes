using Ganss.Excel;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UploadBiomentrics.Model
{
    [BsonCollection("biometricsData")]
    public class BiometricsData : Document
    {
        [Column("AC-No.")]
        public string ElectronicId { get; set; }
        [Column("Date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime LogDate { get; set; }
        [Column("Clock In")]
        public string ClockIn { get; set; }
        [Column("Clock Out")]
        public string ClockOut { get; set; }
    }
}
