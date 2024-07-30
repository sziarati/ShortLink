using Application.Notification;
using Domain.Events;
using MediatR;

namespace Application.ShortLinks.Expired;
public class NotifyUserShortLinkExpiredDomainEventHandler(INotificationService notificationService) : INotificationHandler<NotifyUserShortLinkExpiredDomainEvent>
{
    private readonly INotificationService _notificationService = notificationService;

    public async Task Handle(NotifyUserShortLinkExpiredDomainEvent notification, CancellationToken cancellationToken)
    {
        await _notificationService.Notify(notification.Email, $"dear {notification.UserName} your link {notification.UniqueCode} has been expired.", NotificationType.Email);
    }
}