using Application.Authentication;
using Application.Email;
using Application.Notification;
using Application.SMS;
using Application.Validator;
using FluentValidation;
using MediatR;
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

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        
        return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}