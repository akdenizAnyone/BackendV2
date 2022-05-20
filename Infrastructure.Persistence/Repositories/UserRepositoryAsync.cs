using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories{
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _users;
        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users=dbContext.Set<User>();
        }

        public Task<IEnumerable<User>> GetListOfUser(string search)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByApplicationId(string id)
        {
            var user=await _users.FirstOrDefaultAsync(x=>x.ApplicationUserId==id);
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user =await _users.FirstOrDefaultAsync(x=>x.UserName==username);
            return user;
        }

        public Task<int> GetUserFollowedCount(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetUserFollwersCount(int username)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetUserPostCount(int username)
        {
            throw new System.NotImplementedException();
        }



    }
}