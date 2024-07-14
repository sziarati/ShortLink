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
    public class ShortLinkController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateShortLinkCommand input)
        {
            var createResult = await _mediator.Send(input);
            return createResult.IsSuccess ?
                   Ok(createResult.Data) :
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
        public async Task<IActionResult> Delete([FromBody] DeleteShortLinkCommand input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                    Ok() :
                    BadRequest(result.Message);
        }

        [HttpGet("UniqueCode/{code:regex(^[a-zA-Z0-9]*$)}")]// asp .net route constraint
        public async Task<IActionResult> GetUniqueCode([FromQuery] GetUniqueCodeQuery input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess ?
                   Ok(result) :
                   NotFound();
        }

        [HttpGet("OriginCode/{code:regex(^[a-zA-Z0-9]*$)}")]
        public async Task<IActionResult> GetOriginUrl([FromQuery] GetOriginUrlQuery input)
        {
            var result = await _mediator.Send(input);
            return result.IsSuccess && !string.IsNullOrEmpty(result.Data) ?
                   Redirect(result.Data) :
                   RedirectPermanent("");
        }
    }
}