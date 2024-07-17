using Application.Notification;
using Domain.Events.ShortLinkExpired;
using MediatR;

namespace Application.ShortLinks.Expired;
public class CourseCompletedDomainEventHandler(INotificationService notificationService) : INotificationHandler<ShortLinkExpiredEvent>
{
    private readonly INotificationService _notificationService = notificationService;

    public async Task Handle(ShortLinkExpiredEvent notification, CancellationToken cancellationToken)
    {
        await _notificationService.Notify(notification.Email, $"dear {notification.UserName} your link {notification.UniqueCode} has been expired.", NotificationType.Email);
    }
}