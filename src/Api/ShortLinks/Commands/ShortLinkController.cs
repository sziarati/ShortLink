using Application.ShortLinks.Create;
using Application.ShortLinks.Delete;
using Application.ShortLinks.Expire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.ShortLinks.Commands
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShortLinkController(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateShortLinkCommand input)
        {
            var createResult = await _mediator.Send(input);
            return createResult.IsSuccess ?
                   Results.Created("/api/ShortLink", createResult.Data) :
                   Results.BadRequest(createResult.Message);
        }

        [HttpPut]
        public async Task<IResult> Expire([FromBody] ExpireShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return result ?
                   Results.Ok() :
                   Results.BadRequest();
        }

        [HttpDelete]
        public async Task<IResult> Delete([FromBody] DeleteShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return result ?
                    Results.Ok("/api/ShortLink") :
                    Results.BadRequest("Creation failed.");
        }
    }
}