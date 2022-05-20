using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories{
    public interface IUserRepositoryAsync:IGenericRepositoryAsync<User>{
        
        
        // public Task<IReadOnlyList<Event>> GetEventsWithRelationsAsync(int pageNumber, int pageSize);
        public Task<User> GetUserByApplicationId(string id);
        public Task<User> GetUserByUsername(string username);
        public Task<int> GetUserFollowedCount(int id);
        public Task<int> GetUserFollwersCount(int username);
        public Task<int> GetUserPostCount(int username);
        public Task<IEnumerable<User>> GetListOfUser(string search);
    }

}