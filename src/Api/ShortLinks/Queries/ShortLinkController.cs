using Application.ShortLinks.GetOriginUrl;
using Application.ShortLinks.GetUniqueCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.ShortLinks.Queries
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShortLinkController(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("/getUniqueCode")]
        public async Task<IResult> GetUniqueCode([FromBody] GetUniqueCodeQuery input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                   Results.Ok(result) :
                   Results.NotFound();
        }

        [HttpGet("/getOriginCode")]
        public async Task<IResult> GetOriginUrl([FromBody] GetOriginUrlQuery input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                   Results.Ok(result) :
                   Results.RedirectToRoute();
        }
    }
}