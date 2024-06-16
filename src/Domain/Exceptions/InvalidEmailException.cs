namespace Domain.Exceptions;

public class InvalidEmailException(string ParameterName) : DomainException($"Email {ParameterName} Is Invalid!");
