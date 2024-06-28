using Domain.Guards;

namespace Domain.Entities.ValueObjects;

public record PostalCode
{
    public string Value { get; }
    public PostalCode(string value)
    {
        Guard.AgainstInvalidPostalCode(value, nameof(value));
        Value = value;
    }
    public override string ToString()
    {
        return Value;
    }
    public static PostalCode FromString(string postalCodeString)
    {
        return new PostalCode(postalCodeString);
    }
}
