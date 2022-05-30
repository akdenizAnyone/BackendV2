using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Users.Command;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Users.ViewModels;
using System.Collections.Generic;



namespace WebApi.Controllers.v1{

    [ApiVersion("1.0")]
    [Authorize]
    public class UsersController:BaseApiController{

        
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateUserCommand commnad){
            return await Mediator.Send(commnad);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Update(UpdateUserCommand commnad){
            return await Mediator.Send(commnad);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>>  Get(int id){
            return await Mediator.Send(new GetUserQuery{Id=id});
        }
        [HttpGet]
        public async Task<ActionResult<UserDto>> Get(string id){
            return await Mediator.Send(new GetUserByApplicationIdQuery{ApplicationUserId=id});
        } 
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<UserProfileVM>> GetUserProfile([FromQuery]string username)
        {
            var query = new GetUserProfileQuery {Username = username };
            return await Mediator.Send(query);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserSearchDto>>> SearchUser([FromQuery]string q)
        {
            var query = new UserSearchQuery { Search = q };
            return Ok(await Mediator.Send(query));
        }
    }
}