using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories{
    public class PostRepositoryAsync : GenericRepositoryAsync<User>,IPostRepositoryAsync 
    {
        private readonly DbSet<Post> _posts;
        public PostRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _posts=dbContext.Set<Post>();
        }

        public bool AuthorFollowedBy(int postId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public int CountAnswers(int postId)
        {
            return 4;
        }

        public int CountLikes(int postId)
        {
            return 5;
        }

        public int CountReposts(int postId)
        {
            return 5;
        }

        public User GetUserWhoLikedFollowedBy(int postId, int userId)
        {
            return new User();
        }

        public User GetUserWhoRepostedFollowedBy(int postId, int userId)
        {
            return new User();
        }

        public bool IsLikedBy(int postId, int userId)
        {
            return false;
        }

        public bool IsRepostedBy(int postId, int userId)
        {
            return true;
        }

        public bool LikedBySomeoneFollowedBy(int postId, int userId)
        {
            return false;
        }

        public bool RepostedBySomeoneFollowedBy(int postId, int userId)
        {
            return false;
        }
    }
}