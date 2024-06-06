using Domain.Entities.UserAggregate;

namespace Domain.Events;

public class ShortLinkExpiring : IDomainEvent
{
    public ShortLink ShortLink { get; set; }
}