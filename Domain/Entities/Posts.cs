using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Domain.Entities
{
    public class Post: AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Guid? MediaId { get; set; }
        public Media Media { get; set; }
        public int? AnswerToId { get; set; }
        public Post? AnswerTo { get; set; }
        public IEnumerable<Post> Answers { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public IEnumerable<RePost> RePosts { get; set; }

        public int? UserId{get;set;}
        public User? User{get;set;}

        public static Expression<Func<Post,bool>> LikedBySomeoneFollowedBy(int userId) {
            return p => p.Likes.Any(l => l.User.Followers.Any(f => f.FollowerId == userId));
        }

        public static Expression<Func<Post,bool>> RePostedBySomeoneFollowedBy(int userId) {
            return p => p.RePosts.Any(r => r.User.Followers.Any(f => f.FollowerId == userId));
        }

        public static Expression<Func<Post,bool>> AuthorFollowedBy(int userId) {
            return p => p.User.Followers.Any(f => f.FollowerId == userId);
        }

        public static Expression<Func<Post, User>> GetUserWhoLikedFollowedBy(int userId) {
            return p => p.Likes.Select(l => l.User).FirstOrDefault(u => u.Followers.Any(f => f.FollowerId == userId));
        }

        public static Expression<Func<Post, User>> GetUserWhoRePostedFollowedBy(int userId) {
            return p => p.RePosts.Select(r => r.User).FirstOrDefault(u => u.Followers.Any(f => f.FollowerId == userId));
        }

        public static Expression<Func<Post, bool>> IsLikedBy(int userId) {
            return p => p.Likes.Any(l => l.UserId   == userId);
        }

        public static Expression<Func<Post, bool>> IsRePostedBy(int userId) {
            return p => p.RePosts.Any(r => r.UserId== userId);
        }
    }
}
