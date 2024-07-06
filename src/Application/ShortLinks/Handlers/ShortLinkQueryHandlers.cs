using Application.ShortLinks.Queries;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.ShortLinks.Handlers;


public class ShortLinkQueryHandlers(IUnitOfWork unitOfWork) :
    IRequestHandler<GetOriginUrlQuery, string>
{
    public async Task<string> Handle(GetOriginUrlQuery request, CancellationToken cancellationToken)
    {
        //return await unitOfWork._shortLinkRepository.Get( x => x.UniqueCode == request.ShortLink);
    }
}
