namespace Domain.Exceptions;

public class InvalidPostalCodeException(string value) : DomainException($"PostalCode '{value}' Is invalid!");
