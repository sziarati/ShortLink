namespace Application.Notification;

public class NotificationService(NotificationFactory notificationFactory): INotificationService
{
    private NotificationFactory _notificationFactory = notificationFactory;

    public async Task Notify(string to, string message, NotificationType notificationType)
    {
        var strategy = _notificationFactory.GetStrategy(notificationType);
        await strategy.Notify(to, message);
    }
}

public class Test
{
    public void method(INotificationService notificationService)
    {
        notificationService.Notify("", "", NotificationType.Email);
    }
}