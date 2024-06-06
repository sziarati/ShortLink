using Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.DomainEvents;

public class DomainEventDispatcher(IServiceProvider serviceProvider)
{
    public void Dispatch(IEnumerable<object> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler != null)
                    ((dynamic)handler).Handle((dynamic)domainEvent);
            }
        }
    }
}
