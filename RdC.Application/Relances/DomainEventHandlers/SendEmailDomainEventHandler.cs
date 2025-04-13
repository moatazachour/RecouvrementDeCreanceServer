using MediatR;
using RdC.Application.Common.Email;
using RdC.Domain.Relances.Events;

namespace RdC.Application.Relances.DomainEventHandlers
{
    internal sealed class SendEmailDomainEventHandler
        : INotificationHandler<SendEmailDomainEvent>
    {
        private readonly IEmailService _emailService;

        public SendEmailDomainEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(SendEmailDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailAsync(
                notification.email,
                "Rappel de Paiement",
                notification.emailBody);
        }
    }
}
