using Domain.Bases;
using Domain.Entities.UserAggregate;

namespace Domain.Entities.ShortLinkAggregate;

public partial class ShortLink : BaseEntity, IAggregateRoot
{
    private static readonly int Length = 10;
    private static readonly int ExpiryDays = 2;

    public DateTime ExpireDate { get; private set; }

    public string Name { get; private set; }
    public string OriginUrl { get; private set; }
    public string UniqueCode { get; private set; }

    public uint UserId { get; private set; }
    public User User { get; private set; }
    public bool IsExpired { get; private set; } 
}
