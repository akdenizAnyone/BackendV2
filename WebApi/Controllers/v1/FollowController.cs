using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Follows.Commands;
using Application.Features.Follows.Dtos;
using Application.Features.Follows.Queries;
using Application.Features.Follows.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{

    [ApiVersion("1.0")]

    [Authorize]
    public class FollowController : BaseApiController
    {

        [HttpGet("~/api/users/{userId}/follows")]
        public async Task<FollowsVM> GetUserFollows(int userId)
        {
            var query = new GetUserFollowsQuery { UserId = userId };

            return await Mediator.Send(query);
        }

        [HttpGet("suggestions")]
        public async Task<IEnumerable<SuggestionUserDto>> GetSuggestions(int? count)
        {
            var query = new GetFollowSuggestionsQuery { Count = count };

            return await Mediator.Send(query);
        }


        [HttpPost("~/api/users/{userId}/follows")]
        public async Task<ActionResult> FollowUser(int userId)
        {
            var command = new FollowUserCommand { UserId = userId };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("~/api/users/{userId}/unfollow")]
        public async Task<ActionResult> UnfollowUser(int userId)
        {
            var command = new UnFollowUserCommand { UserId = userId };

            await Mediator.Send(command);

            return Ok();
        }

    }
}