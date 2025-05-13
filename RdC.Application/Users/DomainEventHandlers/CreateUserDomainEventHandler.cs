using MediatR;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;
using RdC.Domain.Users.Events;
using System.Text;

namespace RdC.Application.Users.DomainEventHandlers
{
    internal sealed class CreateUserDomainEventHandler
        : INotificationHandler<CreateUserDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public CreateUserDomainEventHandler(
            IUserRepository userRepository, 
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(CreateUserDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(notification.userID);

            if (user is null)
            {
                return;
            }

            string email = user.Email;
            string emailBody = _BuildEmailBody(user);
            string subject = "Terminer votre inscription sur la plateforme RdC";

            await _emailService.SendEmailAsync(
                to: email,
                subject: subject,
                body: emailBody);
        }

        private string _BuildEmailBody(User user)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Bonjour,");
            sb.AppendLine();
            sb.AppendLine("Un compte a été créé pour vous sur notre plateforme de gestion.");
            sb.AppendLine("Pour activer votre compte, veuillez finaliser votre inscription en cliquant sur le lien ci-dessous :");
            sb.AppendLine();
            sb.AppendLine("[LIEN POUR TERMINER L’INSCRIPTION]");
            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }
    }
}
