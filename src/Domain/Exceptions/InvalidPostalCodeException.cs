namespace Domain.Exceptions;

public class InvalidPostalCodeException(string ParameterName) : DomainException($"PostalCode {ParameterName} Is not in a correct format!");
