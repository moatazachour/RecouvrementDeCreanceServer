using System.Net.Mail;
using System.Net;
using RdC.Application.Common.Email;

namespace RdC.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "moatazachour84@gmail.com";
        private readonly string _stmpPass = "wmrw bmze vmjv lirg";



        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpUser, _stmpPass);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add(to);

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }
    }
}
