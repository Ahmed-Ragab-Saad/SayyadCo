namespace SayyadCo.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string message);
    }
}
