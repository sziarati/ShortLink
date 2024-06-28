namespace Application.SMS.Interfaces
{
    public interface ISmsProviderService
    {
        public Task<bool> Send(string address, string message);
    }
}
