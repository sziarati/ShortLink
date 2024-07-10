using Application.Notification;
using Application.SMS.Interfaces;

namespace Application.SMS;

public class SmsNotificationStrategy(ISmsProviderService smsProvider) : INotificationStrategy
{
    public async Task Notify(string to, string message)
    {
        await smsProvider.Send(to, message);
    }
}