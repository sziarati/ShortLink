namespace Application.Notification;

public interface INotificationService
{
    public Task Notify(string email , string message, NotificationType notificationType);
}
