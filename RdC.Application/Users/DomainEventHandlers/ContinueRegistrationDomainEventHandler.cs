using MediatR;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;
using RdC.Domain.Users.Events;
using System.Text;

namespace RdC.Application.Users.DomainEventHandlers
{
    internal sealed class ContinueRegistrationDomainEventHandler
        : INotificationHandler<ContinueRegistrationDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public ContinueRegistrationDomainEventHandler(
            IUserRepository userRepository, 
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(ContinueRegistrationDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(notification.userID);

            if (user is null)
            {
                return;
            }

            string email = user.Email;
            string emailBody = _BuildEmailBody(user);
            string subject = $"Bienvenue {user.Username}!";

            await _emailService.SendEmailAsync(
                to: email,
                subject: subject,
                body: emailBody);
        }

        private string _BuildEmailBody(User user)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"WELCOME MESSAGE");

            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }
    }
}
