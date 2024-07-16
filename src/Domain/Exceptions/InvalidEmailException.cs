namespace Domain.Exceptions;

public class InvalidEmailException(string value) : DomainException($"Email '{value}' Is Invalid!");
