using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories{
    public interface IPostRepositoryAsync:IGenericRepositoryAsync<User>{
        
       public bool IsLikedBy(int postId,int userId);         
       public bool IsRepostedBy(int postId,int userId);         
       public bool LikedBySomeoneFollowedBy(int postId,int userId);         
       public bool RepostedBySomeoneFollowedBy(int postId,int userId);         
       public bool AuthorFollowedBy(int postId,int userId);         
       public User GetUserWhoLikedFollowedBy(int postId,int userId);         
       public User GetUserWhoRepostedFollowedBy(int postId,int userId);         
       public int CountLikes(int postId);
       public int CountReposts(int postId);
       public int CountAnswers(int postId);

    }

}