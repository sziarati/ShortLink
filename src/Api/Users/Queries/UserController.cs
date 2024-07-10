using Application.Users.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Users.Queries
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IResult> Login([FromBody] LoginUserCommand input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                    Results.Ok(result.Data) :
                    Results.BadRequest(result.Message);
        }
    }
}