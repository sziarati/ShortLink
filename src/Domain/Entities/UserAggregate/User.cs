
using Domain.Bases;
using Domain.Entities.ValueObjects;

namespace Domain.Entities.UserAggregate;

public partial class User : BaseEntity, IAggregateRoot
{
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Address Address { get; set; }
    public byte State { get; private set; }

    public IReadOnlyList<ShortLink>? ShortLinks => _ShortLinks;
    private List<ShortLink>? _ShortLinks { get; set; } = [];
}
