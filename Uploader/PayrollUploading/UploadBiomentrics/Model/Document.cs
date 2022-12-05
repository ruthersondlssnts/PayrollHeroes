using MongoDB.Bson.Serialization.Attributes;
using System;

namespace UploadBiomentrics.Model
{
    public abstract class Document
    {
        public Guid Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow.Date;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DateDeleted { get; set; }
    }
}
