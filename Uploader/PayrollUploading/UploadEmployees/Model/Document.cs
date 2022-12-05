using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UploadEmployees.Model
{
    public abstract class Document
    {
        public Guid Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DateDeleted { get; set; }
    }
}
