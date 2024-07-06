using Application.ShortLinks.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortLinkController
    {
        [HttpPost]
        [Route("/api/ShortLink")]
        public async Task<IResult> Create([FromBody] CreateShortLinkCommand input, IMediator mediator)
        {
            var createResult = await mediator.Send(input);
            return createResult > 0 ?
                   Results.Created("/api/ShortLink", createResult) :
                   Results.BadRequest("Creation failed.");
        }

        [HttpDelete]
        [Route("/api/ShortLink")]
        public async Task<IResult> Delete([FromBody] DeleteShortLinkCommand input, IMediator mediator)
        {
            var result = await mediator.Send(input);
            return result ?
                    Results.Ok("/api/ShortLink") :
                    Results.BadRequest("Creation failed.");
        }

        [HttpGet]
        [Route("/api/ShortLink")]
        public async Task<IResult> Get([FromBody] CreateShortLinkCommand input, IMediator mediator)
        {
            var createResult = await mediator.Send(input);
            return createResult > 0 ?
                   Results.BadRequest("Creation failed.") :
                   Results.Created("/api/ShortLink", createResult);
        }
    }
}