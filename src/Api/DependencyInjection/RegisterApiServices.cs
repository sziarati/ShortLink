using Api.ExceptionHandler;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Api.DependencyInjection;

public static class DependencyExtension
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.AddOptions<Authentications>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });

        services.AddControllers(config =>
        {
            config.Filters.Add(typeof(ResultFilter), 1 /*order*/);
        });

        return services;
    }
}