namespace Domain.Exceptions;

public class InvalidPasswordException(string ParameterName) : DomainException($"Password  {ParameterName} Is Invalid!");
