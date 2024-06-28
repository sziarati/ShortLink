using Application.ShortLinks.Commands;
using Domain.Entities.ShortLinkAggregate;
using Domain.Interfaces.Repository;

using MediatR;

namespace Application.ShortLinks.Handlers;

public class ShortLinkCommandHandlers(IUnitOfWork unitOfWork) :
    IRequestHandler<CreateShortLinkCommand, uint>,
    IRequestHandler<DeleteShortLinkCommand, bool>
{
    public async Task<uint> Handle(CreateShortLinkCommand request, CancellationToken cancellationToken)
    {
        var shortLink = new ShortLink(request.Name, request.OriginalUrl);
        await unitOfWork._shortLinkRepository.AddAsync(shortLink);
        var result = await unitOfWork.SaveChangesAsync();
        return result > 0 ? shortLink.Id : default;
    }

    public async Task<bool> Handle(DeleteShortLinkCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork._shortLinkRepository.Delete(request.Id);
        var result = await unitOfWork.SaveChangesAsync();
        return result > 0;
    }
}
