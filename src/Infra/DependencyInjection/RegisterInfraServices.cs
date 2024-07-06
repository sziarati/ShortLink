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

namespace Application.DependencyExtension;
public static class DependencyExtension
{
    public static IServiceCollection RegisterInfraServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(config =>
        config.UseSqlServer($"Server=.;Database=Clean;User Id=sa;Password=!@#123qwe;Encrypt=False;"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShortLinkRepository, ShortLinkRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ISmsProviderService, SmsService>();

        return services;
    }
}
