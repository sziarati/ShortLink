using Microsoft.Extensions.DependencyInjection;

namespace Application.Notification;

public class NotificationFactory(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    public INotificationStrategy GetStrategy(NotificationType notificationType)
    {
        return _serviceProvider.GetRequiredKeyedService<INotificationStrategy>(notificationType);
    }
}
