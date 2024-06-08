using Application.Interfaces.Services;
using Domain.Interfaces.Repository;
using Infra.Data;
using Infra.Data.Repository;
using Infra.Features.Notification;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddDbContext<AppDbContext>(config =>
        config.UseSqlServer($"Server=.;Database=Clean;User Id=sa;Password=!@#123qwe;Encrypt=False;"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
