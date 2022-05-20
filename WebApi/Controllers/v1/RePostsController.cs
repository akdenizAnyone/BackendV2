using System.Threading.Tasks;
using Application.Features.RePosts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{

    [ApiVersion("1.0")]

    public class RePostsController : BaseApiController
    {


        [HttpPost("~/api/posts/{postId}/re-post")]
        public async Task CreateRePost(int postId)
        {
            var command = new CreateRePostCommand { PostId = postId };

            await Mediator.Send(command);
        }

        [HttpDelete("~/api/posts/{postId}/re-post")]
        public async Task RemoveRePost(int postId)
        {
            var command = new RemoveRePostCommand { PostId = postId };

            await Mediator.Send(command);
        }
    }
}