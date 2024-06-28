using Domain.Guards;

namespace Domain.Entities.ValueObjects;

public class Email
{
    public string Value { get; }
    public Email(string value)
    {
        Guard.AgainstInvalidEmail(value, nameof(value));
        Value = value;
    }
    public override string ToString()
    {
        return Value;
    }
    public static Email FromString(string emailString)
    {
        return new Email(emailString);
    }
}
