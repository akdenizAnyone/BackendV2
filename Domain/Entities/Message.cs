using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Message:AuditableBaseEntity
    {
        public string Content { get; set; }
        public Media Media { get; set; }
        public Guid? MediaId { get; set; }
    }
}