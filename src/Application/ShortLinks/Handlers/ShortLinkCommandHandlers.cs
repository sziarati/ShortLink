using Application.Notification;
using Application.ShortLinks.Commands;
using Domain.Entities.ShortLinkAggregate;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.ShortLinks;
using FluentValidation;
using MediatR;

namespace Application.ShortLinks.Handlers;

public class ShortLinkCommandHandlers(IUnitOfWork _unitOfWork, IShortLinkRepository _shortLinkRepository, INotificationService notificationService) :
    IRequestHandler<CreateShortLinkCommand, int>,
    IRequestHandler<DeleteShortLinkCommand, bool>
{
    public async Task<int> Handle(CreateShortLinkCommand request, CancellationToken cancellationToken)
    {
        await notificationService.Notify("", "", NotificationType.Email);
        var shortLink = new ShortLink(request.Name, request.OriginalUrl);
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
}