using System.Runtime.Serialization;

namespace Application.Validator.Exceptions;

internal class ValidationException : Exception
{
    private List<ValidationError> errors;

    public ValidationException()
    {
    }

    public ValidationException(List<ValidationError> errors)
    {
        this.errors = errors;
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}