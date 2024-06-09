using Domain.Guards;

namespace Domain.Entities.ValueObjects;

public record Email
{
    public string Value { get; }
    public Email(string value)
    {
        Guard.AgainstInvalidEmail(value, nameof(value));
        Value = value;
    }
}
