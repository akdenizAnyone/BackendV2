using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Comment: AuditableBaseEntity
    {
        public string Content { get; set; }
        public int Liked { get; set; }
        public int PostId{get;set;}
        public Post Post{get;set;}
        public ICollection<Comment>? Comments{get;set;}
    }
}
