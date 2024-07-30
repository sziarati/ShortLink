using Application.Results;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Users.Delete;

public record DeleteUserCommand(int Id) : IRequest<Result<bool>>, IParsable<DeleteUserCommand>
{
    public static DeleteUserCommand Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DeleteUserCommand result)
    {
        throw new NotImplementedException();
    }
}

