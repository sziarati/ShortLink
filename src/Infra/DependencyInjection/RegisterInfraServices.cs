using Application.Email.Interfaces;
using Application.SMS.Interfaces;
using Domain.Interfaces.Repository.ShortLinks;
using Domain.Interfaces.Repository.Users;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using Infra.Data.Repositories.Users;
using Infra.Data.Repositories.ShortLinks;
using Infra.Data.Repositories;
using Infra.Features.Email;
using Infra.Features.SMS;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using Infra.Services.Jobs;

namespace Application.DependencyExtension;
public static class DependencyExtension
{
    public static IServiceCollection RegisterInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(config =>
        config.UseSqlServer($"Server=.;Database=Clean;User Id=sa;Password=!@#123qwe;Encrypt=False;"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShortLinkRepository, ShortLinkRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ISmsProviderService, SmsService>();

        services.AddScoped<IJob, CheckAndExpireShortLinksJob>();
        services.AddHostedService<RunJobsHostedService>();

        services.AddHangfire(config => config
                .UseSqlServerStorage(configuration.GetConnectionString("Hangfire")));

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        return services;
    }

    public static IEndpointRouteBuilder EndpointRouteBuilder(this IEndpointRouteBuilder builder)
    {
        // HangFire Dashboard endpoint
        builder.MapHangfireDashboard();
        return builder;
    }

}
