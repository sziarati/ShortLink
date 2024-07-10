using Application.Results;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.GetUniqueCode;

public class GetUniqueCodeHandler(IShortLinkRepository shortLinkRepository) :
    IRequestHandler<GetUniqueCodeQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetUniqueCodeQuery request, CancellationToken cancellationToken)
    {
        var shortLinkResult = await shortLinkRepository.GetByOriginUrlAsync(request.OriginUrl, cancellationToken);
        return shortLinkResult == null ?
                Result<string>.Failure(Errors.NotFoundError):
                Result<string>.Success(shortLinkResult.UniqueCode);
    }
}
