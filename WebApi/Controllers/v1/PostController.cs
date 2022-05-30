using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Users.Command;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Users.ViewModels;
using System.Collections.Generic;
using Application.Features.Posts.Dtos;
using Application.Features.Posts.Queries;
using Application.Features.Posts.Command;
using Application.Interfaces;

namespace WebApi.Controllers.v1{

    [ApiVersion("1.0")]
    public class PostController:BaseApiController{
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public PostController(IAuthenticatedUserService authenticatedUserService)
        {
            _authenticatedUserService=authenticatedUserService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto2>> Get([FromQuery] GetPostsQuery query) {
            return await Mediator.Send(query);
        }   

        [HttpGet("{id}")]
        public async Task<PostDto> Get(int id) {
            return await Mediator.Send(new GetPostQuery { Id = id });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> Create(CreatePostCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("~/api/users/{userId}/posts")]
        public async Task<IEnumerable<PostDto>> GetUserPosts(int userId, int? beforeId, int? count)
        {
            var query = new GetUserPostsQuery{ UserId = userId, BeforeId = beforeId, Count = count };
            return await Mediator.Send(query);
        }

        [HttpGet("{postId}/answers")]
        public async Task<IEnumerable<PostDto>> GetPostAnswers(int postId, int? beforeId, int? count)
        {
            var query = new GetPostAnswerQuery{ PostId = postId, BeforeId = beforeId, Count = count };
            return await Mediator.Send(query);
        }

    }
}