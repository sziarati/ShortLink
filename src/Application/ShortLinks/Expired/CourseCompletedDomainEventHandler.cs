using Application.Notification;
using Domain.Events.ShortLinkExpired;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.Expired;
public class CourseCompletedDomainEventHandler(IShortLinkRepository shortLinkRepository, INotificationService notificationService) : INotificationHandler<ShortLinkExpiredEvent>
{
    private readonly IShortLinkRepository _shortLinkRepository = shortLinkRepository;
    private readonly INotificationService _notificationService = notificationService;

    public async Task Handle(ShortLinkExpiredEvent notification, CancellationToken cancellationToken)
    {
        var shortLink = notification.shortLink;
        var user = shortLink.User;
        await _notificationService.Notify(user.Email.Value, $"dear {user.UserName} your link {shortLink.UniqueCode} has been expired.", NotificationType.Email);

        //await _shortLinkRepository.SetExpired(shortLink.Id);
    }
}