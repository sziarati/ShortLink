namespace Application.Email.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> Send(string address, string message);
    }
}
