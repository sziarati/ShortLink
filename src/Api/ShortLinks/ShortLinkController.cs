using Api.Bases;
using Application.ShortLinks.Create;
using Application.ShortLinks.Delete;
using Application.ShortLinks.Expire;
using Application.ShortLinks.GetOriginUrl;
using Application.ShortLinks.GetUniqueCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.ShortLinks
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShortLinkController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ResultFilter]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateShortLinkDTO input)
        {
            var command = new CreateShortLinkCommand(
                input.Name,
                input.OriginUrl,
                CurrentUserId
            );

            var createResult = await _mediator.Send(command);
            return Ok(createResult);
        }

        [HttpPut]
        [ResultFilter]
        [Authorize]
        public async Task<IActionResult> Expire([FromBody] ExpireShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return Ok(result.IsSuccess);
        }

        [HttpDelete]
        [ResultFilter]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] DeleteShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }

        [HttpGet("/originUrl/{OriginUrl:regex(^[[a-zA-Z0-9]]*)}")]
        [ResultFilter]
        public async Task<IActionResult> GetUniqueCode([FromRoute] GetUniqueCodeQuery input)
        {
            var result = await _mediator.Send(input);
            return Ok(result);
        }

        [HttpGet("{UniqueCode:regex(^[[a-zA-Z0-9]]*)}")]
        public async Task<IActionResult> GetOriginUrl([FromRoute] GetOriginUrlQuery input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess && !string.IsNullOrEmpty(result.Data) ?
                   Redirect(result.Data) :
                   RedirectPermanent("http://www.google.com");
        }
    }
}