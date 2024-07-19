using Application.Notification;
using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Login;
using Application.Users.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Users
{
    [ApiController]
    [ResultFilter]
    [Route("/api/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand input)
        {
            var createResult = await _mediator.Send(input);
            return Ok(createResult);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserCommand input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordCommand input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] LoginUserCommand input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }
    }
}