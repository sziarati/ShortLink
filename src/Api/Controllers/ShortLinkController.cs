using Application.ShortLinks.Commands;
using Application.ShortLinks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShortLinkController(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("Create")]
        public async Task<IResult> Create([FromBody] CreateShortLinkCommand input)
        {
            var createResult = await _mediator.Send(input);
            return createResult > 0 ?
                   Results.Created("/api/ShortLink", createResult) :
                   Results.BadRequest("Creation failed.");
        }

        [HttpPost("Expire")]
        public async Task<IResult> Expire([FromBody] ExpireShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return result ?
                   Results.Ok() :
                   Results.BadRequest();
        }

        [HttpPost("CheckAndExpire")]
        public async Task<IResult> CheckAndExpire([FromBody] CheckAndExpireShortLinkCommand input)
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

        [HttpGet]
        public async Task<IResult> GetUniqueCode([FromBody] GetUniqueCodeQuery input)
        {
            var result = await _mediator.Send(input);
            return !string.IsNullOrEmpty(result) ?
                   Results.Ok(result) :
                   Results.NotFound();
        }

        [HttpGet]
        public async Task<IResult> GetOriginUrl([FromBody] GetOriginUrlQuery input)
        {
            var result = await _mediator.Send(input);
            return !string.IsNullOrEmpty(result) ?
                   Results.Ok(result) :
                   Results.NotFound();
        }
    }
}