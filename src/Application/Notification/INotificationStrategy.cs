namespace Application.Notification;

public interface INotificationStrategy
{
    public Task Notify(string to, string message);
}
