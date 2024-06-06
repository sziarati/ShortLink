using Domain.Events;
using Infra.Features.Notification;

namespace Infra.DomainEvents;

public class ShortLinkExpiringEventHandler(NotificationService notificationService) : IHandler<ShortLinkExpiring>
{
    public void Handle(ShortLinkExpiring domainEvent)
    {
        var message = $"Dear {domainEvent.ShortLink.User.UserName}, your shortLink {domainEvent.ShortLink.OriginUrl} will be expired in 2 days!";
        notificationService.SendNotification(domainEvent.ShortLink.User.Email, message);
    }
}
