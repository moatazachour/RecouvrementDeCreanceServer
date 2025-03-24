namespace RdC.Application.Common.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
