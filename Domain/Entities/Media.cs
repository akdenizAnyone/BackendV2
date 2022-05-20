using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Media:AuditableBaseEntity
    {
        public Guid GudId{get;set;}
        public byte[] Content { get; set; }
        public string  ContentType { get; set; }
        public string FileName{get;set;}
    }
}