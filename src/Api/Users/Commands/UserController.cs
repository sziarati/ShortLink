using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Users.Commands
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateUserCommand input)
        {
            var createResult = await _mediator.Send(input);
            return createResult.IsSuccess ?
                   Results.Created("/api/User", createResult.Data) :
                   Results.BadRequest(createResult.Message);
        }

        [HttpDelete]
        public async Task<IResult> Delete([FromBody] DeleteUserCommand input)
        {
            var result = await _mediator.Send(input);
            return result ?
                    Results.Ok() :
                    Results.BadRequest();
        }

        [HttpPut]
        public async Task<IResult> ResetPassword([FromBody] ResetPasswordCommand input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                    Results.Ok() :
                    Results.BadRequest(result.Message);
        }
    }
}