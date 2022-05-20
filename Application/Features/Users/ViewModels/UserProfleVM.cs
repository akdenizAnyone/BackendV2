using System;
using Application.Features.Users.Dtos;

namespace Application.Features.Users.ViewModels{
   public class UserProfileVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public string PictureId { get; set; }
        public string BannerId { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public bool IsCertified { get; set; }
        public int PostsCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowedCount { get; set; }
    } 
}