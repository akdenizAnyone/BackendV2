using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Conversation:AuditableBaseEntity
    {
        public ICollection<UserConversation> Members { get; set; }
        public ICollection<Message> Messages{ get; set; }
    }
}