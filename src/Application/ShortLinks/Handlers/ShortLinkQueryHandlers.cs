using Application.ShortLinks.Queries;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.ShortLinks.Handlers;


public class ShortLinkQueryHandlers(IUnitOfWork unitOfWork) :
    //IRequestHandler<GetShortUrlQuery, string>,
    IRequestHandler<GetOriginUrlQuery, string>//,
    //IRequestHandler<GetUserShortLinksQuery, List<ShortLink>>

{
    //public async Task<string> Handle(GetShortUrlQuery request, CancellationToken cancellationToken)
    //{
    //    return await unitOfWork.GetShortUrl(request, cancellationToken);
    //}

    public async Task<string> Handle(GetOriginUrlQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork._shortLinkRepository.GetByUniqueCodeAsync(request.ShortLink, cancellationToken);
    }

    //public async Task<List<ShortLink>> Handle(GetUserShortLinksQuery request, CancellationToken cancellationToken)
    //{
    //    return await unitOfWork.GetUserShortUrls(request, cancellationToken);
    //}
}
