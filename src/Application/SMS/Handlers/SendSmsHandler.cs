using Application.Notification.Command;
using Application.Notification.Enums;
using Application.SMS.Interfaces;
using MediatR;

namespace Application.SMS.Handlers;

public class SendSmsHandler(ISmsProviderService smsService) : INotificationHandler<SendNotificationCommand>
{
    public async Task Handle(SendNotificationCommand notification, CancellationToken cancellationToken)
    {
        if (notification.type != SendNotificationType.SMS)
            return;

        await smsService.Send(notification.address, notification.message);
    }
}
