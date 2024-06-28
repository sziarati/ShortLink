using Application.Email.Interfaces;
using Application.Notification.Services;
using Application.SMS.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.ShortLinks;
using Domain.Interfaces.Repository.Users;
using Infra.Data;
using Infra.Data.Repositories;
using Infra.Data.Repositories.ShortLinks;
using Infra.Data.Repositories.Users;
using Infra.Features.Email;
using Infra.Features.SMS;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShortLinkRepository, ShortLinkRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmsProviderService, SmsService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddDbContext<AppDbContext>(config =>
        config.UseSqlServer($"Server=.;Database=Clean;User Id=sa;Password=!@#123qwe;Encrypt=False;"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
