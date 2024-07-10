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

    public override string ToString()
    {
        return Value;
    }
    public static Password FromString(string passwordString)
    {
        return new Password(passwordString);
    }

    public bool Equal(object? password)
    {
        if (password == null || password.GetType() != typeof(Password))
            return false;

        var _password = password as Password;

        return _password?.Value == Value;
    }
}
