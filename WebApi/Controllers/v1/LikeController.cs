using System;
using System.Threading.Tasks;
using Application.Features.Likes.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [Authorize]
    public class LikeController : BaseApiController

    {

        [HttpPost("~/api/posts/{postId}/like")]
        public async Task CreateLike(int postId)
        {
            var command = new CreateLikeCommand { PostId = postId };

            await Mediator.Send(command);
        }

        [HttpDelete("~/api/posts/{postId}/like")]
        public async Task RemoveLike(int postId)
        {
            var command = new RemoveLikeCommand { PostId = postId };

            await Mediator.Send(command);
        }
    }

    internal class AuthorizeAttribute : Attribute
    {
    }
}
