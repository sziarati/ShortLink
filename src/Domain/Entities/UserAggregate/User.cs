
using Domain.Bases;
using Domain.Entities.ShortLinkAggregate;
using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace Domain.Entities.UserAggregate;

public partial class User : BaseEntity, IAggregateRoot
{
    public string UserName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Address Address { get; set; }
    public UserStatus State { get; private set; }

    public IReadOnlyList<ShortLink>? ShortLinks => _ShortLinks;
    private List<ShortLink>? _ShortLinks { get; set; } = [];
}
