using Domain.Entities.ShortLinkAggregate;

namespace Domain.Events.ShortLinkExpired;

public record ShortLinkExpiredEvent(ShortLink shortLink) : IDomainEvent;
