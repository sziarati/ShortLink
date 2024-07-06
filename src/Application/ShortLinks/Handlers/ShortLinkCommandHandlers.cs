using Application.ShortLinks.Commands;
using Domain.Entities.ShortLinkAggregate;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.Handlers;

public class ShortLinkCommandHandlers(IUnitOfWork _unitOfWork, IShortLinkRepository _shortLinkRepository) :
    IRequestHandler<CreateShortLinkCommand, int>,
    IRequestHandler<DeleteShortLinkCommand, bool>,
    IRequestHandler<ExpireShortLinkCommand, bool>,
    IRequestHandler<CheckAndExpireShortLinkCommand, bool>

{
    public async Task<int> Handle(CreateShortLinkCommand request, CancellationToken cancellationToken)
    {
        var shortLink = new ShortLink(request.Name, request.OriginUrl);
        await _shortLinkRepository.AddAsync(shortLink);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? shortLink.Id : default;
    }

    public async Task<bool> Handle(DeleteShortLinkCommand request, CancellationToken cancellationToken)
    {
        await _shortLinkRepository.Delete(request.Id);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Handle(ExpireShortLinkCommand request, CancellationToken cancellationToken)
    {
        await _shortLinkRepository.SetExpired(request.id);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Handle(CheckAndExpireShortLinkCommand request, CancellationToken cancellationToken)
    {
        var expiredShortLinks = await _shortLinkRepository.GetAllExpiredShortLinksAsync();
        foreach (var item in expiredShortLinks)
        {
            item.CheckAndExpireShortLink();
        }

        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0;
    }
}