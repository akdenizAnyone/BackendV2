using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.Features.Users.Command;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMediator _meditor;
        public AccountController(IAccountService accountService, IMediator mediator)
        {
            _accountService = accountService;
            _meditor=mediator;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            var result= await _accountService.RegisterAsync(request, origin);
            if(result.Succeeded==true){
                var resultFromUserController=await _meditor.Send(new CreateUserCommand{ 
                    ApplicationId=result.Data.Id,
                    UserName=result.Data.UserName,
                    FullName=result.Data.FullName,
                    Email=result.Data.Email
                    });
            }
            return Ok(result);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery]string userId, [FromQuery]string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            
            return Ok(await _accountService.ResetPassword(model));
        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress==null ? "127.0.0.1":HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}