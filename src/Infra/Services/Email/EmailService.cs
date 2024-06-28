using Application.Email.Interfaces;

namespace Infra.Features.Email;

public class EmailService : IEmailService
{
    public Task<bool> Send(string address, string message)
    {
        throw new NotImplementedException();
    }
}
