using Application.Results;
using Domain.Entities.ValueObjects;

namespace Application.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> Login(string username, Password password);
    Result<CurrentUser> GetCurrentUser();
}
