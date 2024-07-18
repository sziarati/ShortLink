
namespace Domain.Events.ShortLinkExpired;

public record NotifyUserShortLinkExpiredDomainEvent(string UniqueCode, string UserName, string Email) : IDomainEvent;
