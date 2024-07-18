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
    [Route("/api/[controller]")]
    public class UserController(IMediator mediator, INotificationService notificationService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand input)
        {
            await notificationService.Notify("", "", NotificationType.Email);
            var createResult = await _mediator.Send(input);
            return createResult.IsSuccess ?
                   Created("/api/User", createResult.Data) :
                   BadRequest(createResult.Message);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserCommand input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                    Ok() :
                    BadRequest(result.Message);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordCommand input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                   Ok() :
                   BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] LoginUserCommand input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }
    }
}