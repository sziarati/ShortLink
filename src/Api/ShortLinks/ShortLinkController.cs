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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateShortLinkDTO input)
        {
            var command = new CreateShortLinkCommand(
                input.Name,
                input.OriginUrl,
                CurrentUserId
            );

            var createResult = await _mediator.Send(command);
            return createResult.IsSuccess ?
                   Ok(createResult.Data?.UniqueCode) :
                   BadRequest(createResult.Message);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Expire([FromBody] ExpireShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                   Ok() :
                   BadRequest(result.Message);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] DeleteShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                    Ok() :
                    BadRequest(result.Message);
        }

        [HttpGet("/originUrl/{OriginUrl:regex(^[[a-zA-Z0-9]]*)}")]
        public async Task<IActionResult> GetUniqueCode([FromRoute] GetUniqueCodeQuery input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                   Ok(result) :
                   NotFound();
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