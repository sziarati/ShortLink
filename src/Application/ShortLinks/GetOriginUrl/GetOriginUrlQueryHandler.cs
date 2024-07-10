using Application.Results;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.GetOriginUrl;
public class GetOriginUrlQueryHandler(IShortLinkRepository shortLinkRepository) :
    IRequestHandler<GetOriginUrlQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetOriginUrlQuery request, CancellationToken cancellationToken)//301
    {
        var shortLink = await shortLinkRepository.GetByOriginUrlAsync(request.UniqueCode, cancellationToken);
        
        return shortLink is null ? Result<string>.NotFound() : Result<string>.Success(shortLink.OriginUrl);

    }
}
