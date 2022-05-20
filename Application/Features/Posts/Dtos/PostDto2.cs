using System;
using Application.Features.Users.Dtos;

namespace Application.Features.Posts.Dtos
{
    public class PostDto2
    {
        public int Id { get; set; }
        public int? AnswerToId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string MediaId { get; set; }
        public UserDto LikedBy { get; set; }
        public bool LikedByMe { get; set; }
        public int Likes { get; set; }
        public UserDto RePostedBy { get; set; }
        public bool RePostedByMe { get; set; }
        public int RePosts { get; set; }
        public int Answers { get; set; }
    }

}