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
                   Results.BadRequest("Creation failed.") :
                   Results.Created("/api/ShortLink", createResult);
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
