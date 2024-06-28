
namespace Domain.Results;

public class DomainErrors
{
    public string Value { get; }
    public DomainErrors(string value)
    {
        Value = value;
    }
    public static DomainErrors InvalidEmailError = new DomainErrors($"Email Is Invalid!");
    public static DomainErrors InvalidPasswordError = new DomainErrors($"Password Is Invalid!");
    public static DomainErrors InvalidPostalCodeError = new DomainErrors($"PostalCode Is not in a correct format!");

}
