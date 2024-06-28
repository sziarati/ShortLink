using Application.Email.Interfaces;
using Application.Notification.Command;
using Application.Notification.Enums;
using MediatR;

namespace Application.Email.Handlers;

public class SendEmailHandler(IEmailService emailService) : INotificationHandler<SendNotificationCommand>
{
    public async Task Handle(SendNotificationCommand notification, CancellationToken cancellationToken)
    {
        if (notification.type != SendNotificationType.Email)
            return;

        await emailService.Send(notification.address, notification.message);
    }
}