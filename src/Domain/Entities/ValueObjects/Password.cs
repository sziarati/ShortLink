using Domain.Guards;

namespace Domain.Entities.ValueObjects;

public record Password
{
    public string Value { get; }
    public Password(string value)
    {
        Guard.AgainstInvalidPassword(value, nameof(value));
        Value = value;
    }
}
