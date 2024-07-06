using Application.Email.Interfaces;
using Application.Notification;

namespace Application.Email;

public class EmailNotificationStrategy(IEmailService emailService) : INotificationStrategy
{
    public async Task Send(string to, string message)
    {
        await emailService.Send(to, message);
    }
}
