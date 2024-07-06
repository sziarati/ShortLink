namespace Application.Notification;

public interface INotificationStrategy
{
    public Task Send(string to, string message);
}
