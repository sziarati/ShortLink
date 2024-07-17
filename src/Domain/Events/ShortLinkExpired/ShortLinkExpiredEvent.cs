
namespace Domain.Events.ShortLinkExpired;

public record ShortLinkExpiredEvent(string UniqueCode, string UserName, string Email) : IDomainEvent;
