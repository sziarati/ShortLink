using Application.Notification.Enums;
using MediatR;

namespace Application.Notification.Command;

public record SendNotificationCommand(string address, string message, SendNotificationType type) : INotification;