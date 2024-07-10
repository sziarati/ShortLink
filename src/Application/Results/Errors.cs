namespace Application.Results;

public class Errors
{
    public string Value { get; }
    public Errors(string value)
    {
        Value = value;
    }

    public static Errors CreationError = new Errors($"Creation Failed!");
    public static Errors NotFoundError = new Errors($"NotFound!");
    public static Errors ValidationError = new Errors($"your input is invalid!");
    public static Errors TokenIsInvalidError = new Errors($"user's token is invalid!");
    public static Errors LoginFailedError = new Errors($"userName or Password is incorrect!");
    public static Errors InvalidEmailError = new Errors($"Email Is Empty or Invalid!");
    public static Errors InvalidUserNameError = new Errors($"UserName Is Empty or Invalid!");
    public static Errors InvalidPasswordError = new Errors($"Password Is Empty or Invalid!");
    public static Errors InvalidRepeatedPasswordError = new Errors($"Password Repeat Is Invalid!");
    public static Errors InvalidPostalCodeError = new Errors($"PostalCode Is not in a correct format!");

}
