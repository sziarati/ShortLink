namespace Domain.Events;

public record NotifyUserShortLinkExpiredDomainEvent(string UniqueCode, string UserName, string Email) : IDomainEvent;
