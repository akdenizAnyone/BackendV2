using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext ,IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        //mines
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        
        public DbSet<Like>Likes{ get; set; }
        public DbSet<RePost> RePosts{ get; set; }

       public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Anyone Configuration
            // builder.Entity<Follow>()
            //     .HasOne(f=>f.FollowerId)
            //     .WithMany(u=>u.Followers)
            //     .HasForeignKey(f=>f.FollowedId)
            //     .OnDelete(DeleteBehavior.Restrict);

            #region UserFollow
                
            builder.Entity<Follow>()
                .HasOne(f=>f.Followed)
                .WithMany(u=>u.Followers)
                .HasForeignKey(f=>f.FollowId);

            builder.Entity<Follow>()
                .HasOne(f=>f.Follower)
                .WithMany(u=>u.Followeds)
                .HasForeignKey(f=>f.FollowerId);

            #endregion


            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(builder);
        }
    }
}
