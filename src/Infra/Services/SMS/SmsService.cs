using Application.SMS.Interfaces;

namespace Infra.Features.SMS;

public class SmsService : ISmsProviderService
{
    public Task<bool> Send(string address, string message)
    {
        throw new NotImplementedException();
    }
}
