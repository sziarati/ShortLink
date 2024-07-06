using Application.ShortLinks.Queries;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.Handlers;


public class ShortLinkQueryHandlers(IShortLinkRepository shortLinkRepository) :
    IRequestHandler<GetUniqueCodeQuery, string>,
    IRequestHandler<GetOriginUrlQuery, string>
{    
    public async Task<string> Handle(GetUniqueCodeQuery request, CancellationToken cancellationToken)
    {
        return await shortLinkRepository.GetByOriginUrlAsync(request.OriginUrl, cancellationToken);
    }

    public async Task<string> Handle(GetOriginUrlQuery request, CancellationToken cancellationToken)
    {
        return await shortLinkRepository.GetByUniqueCodeAsync(request.UniqueCode, cancellationToken);
    }
}
