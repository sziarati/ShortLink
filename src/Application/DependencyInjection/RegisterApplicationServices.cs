using Application.Authentication;
using Application.Email;
using Application.Notification;
using Application.SMS;
using Application.Validator;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Application.DependencyExtension;

public static class DependencyExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration);
        services.Configure<FeatureConfigurations>(configuration.GetSection("FeatureConfigurations"));

        var featureConfigurations = new FeatureConfigurations();
        configuration
            .GetSection("FeatureConfigurations")
            .Bind(featureConfigurations);

        services.AddHttpContextAccessor();

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

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        var keyBytes = Encoding.ASCII.GetBytes(featureConfigurations.Authentications.JWTKey);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }
}