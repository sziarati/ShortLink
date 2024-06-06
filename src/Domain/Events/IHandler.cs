namespace Domain.Events;

public interface IHandler<T>
{
    void Handle(T domainEvent);
}