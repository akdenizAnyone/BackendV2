using Domain.Common;

namespace Domain.Entities
{
    public class Like:AuditableBaseEntity{
        public int UserId{get;set;}
        public User User{get;set;}
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}