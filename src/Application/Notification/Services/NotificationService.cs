using Application.Notification.Command;
using MediatR;

namespace Application.Notification.Services;

public class NotificationService(IMediator mediator)
{
    public async Task SendNotification(SendNotificationCommand command)
    {
        await mediator.Publish(command);
    }
}
