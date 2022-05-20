using Domain.Common;

namespace Domain.Entities{
    public class UserConversation:AuditableBaseEntity{
        public int UserId{get;set;}
        public int ConversationId{get;set;}
    }

}