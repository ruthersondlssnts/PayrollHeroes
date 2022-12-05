using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Frontend.Infrastructure
{
    public abstract class Document : IDocument
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DateDeleted { get; set; }
    }

    public interface IDocument
    {
        Guid Id { get; set; }

        DateTime CreateDate { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
