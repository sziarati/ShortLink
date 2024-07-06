using Application.Email;
using Application.Notification;
using Application.SMS;
using Application.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.DependencyExtension;

public static class DependencyExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {

        services.AddSingleton<NotificationFactory>();
        services.AddTransient<INotificationService, NotificationService>();

        services.AddKeyedTransient<INotificationStrategy, EmailNotificationStrategy>(NotificationType.Email);
        services.AddKeyedTransient<INotificationStrategy, SmsNotificationStrategy>(NotificationType.Sms);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
