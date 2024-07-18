using Microsoft.Extensions.DependencyInjection;

namespace Application.Notification;

public class NotificationFactory(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    public INotificationStrategy GetStrategy(NotificationType notificationType)
    {
        using (var scope = _serviceProvider.CreateAsyncScope())
        {
            return scope.ServiceProvider.GetRequiredKeyedService<INotificationStrategy>(notificationType);
        }
    }
}
