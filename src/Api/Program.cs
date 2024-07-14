using Api;
using Api.ExceptionHandler;
using Application.DependencyExtension;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.RegisterApplicationServices(builder.Configuration);
builder.Services.RegisterInfraServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.EndpointRouteBuilder();
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});
app.Run();
