using System;

namespace Application.Features.Users.Dtos{

    public class ProfileUserDto   { 
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
    }
}
