using Microsoft.Extensions.DependencyInjection;

namespace Application.Notification;

public class NotificationService(NotificationFactory notificationFactory): INotificationService
{
    private NotificationFactory _notificationFactory = notificationFactory;

    public async Task Notify(string to, string message, NotificationType notificationType)
    {
        var strategy = _notificationFactory.GetStrategy(notificationType);
        await strategy.Send(to, message);
    }
}

public interface INotificationService
{
    public Task Notify(string to, string message, NotificationType notificationType);
}

public class NotificationFactory(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    public INotificationStrategy GetStrategy(NotificationType notificationType)
    {
        return _serviceProvider.GetRequiredKeyedService<INotificationStrategy>(notificationType);
    }
}

public class Test
{
    public void method(INotificationService notificationService)
    {
        notificationService.Notify("", "", NotificationType.Email);
    }
}