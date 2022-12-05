using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refCalendarActivities")]
    public class RefCalendarActivities : Document
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly = true)]
        public DateTime WorkDate { get; set; }
        public string description { get; set; }
        public int HolidayTypeId { get; set; }
        public string HolidayTypeName { get; set; }
    }
}
