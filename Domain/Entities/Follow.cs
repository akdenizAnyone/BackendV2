using Domain.Common;

namespace Domain.Entities
{
    public class Follow:AuditableBaseEntity
    {
        public int FollowerId { get; set; }
        public User Follower { get; set; }
        public int FollowId { get; set; }
        public User Followed { get; set; }
    }
}