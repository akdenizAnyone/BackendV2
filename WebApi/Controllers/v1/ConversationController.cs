using System.Threading.Tasks;
using Application.Features.Conversations.Commands;
using Application.Features.Likes.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{

    [ApiVersion("1.0")]

    public class ConversationController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateConversationCommand command) 
        {
            return await Mediator.Send(command);
        }
    }
}
