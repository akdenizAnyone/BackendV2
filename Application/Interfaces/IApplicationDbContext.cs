using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces{

    public interface IApplicationDbContext
    {
        DbSet<Conversation> Conversations { get; set; }
        DbSet<User> Users{ get; set; }
        DbSet<Follow> Follows { get; set; }
        DbSet<Media> Medias { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Post> Posts { get; set; }
        // DbSet<RePost> RePosts{ get; set; }
        DbSet<Like> Likes{ get; set; }
        DbSet<RePost> RePosts{ get; set; }
        

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}