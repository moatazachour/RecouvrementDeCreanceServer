using MediatR;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
using RdC.Domain.PaiementDates;
using RdC.Domain.Paiements;
using RdC.Domain.Paiements.Events;
using RdC.Domain.PlanDePaiements;
using System.Text;

namespace RdC.Application.Paiements.DomainEventHandlers
{
    internal sealed class CreatePaiementDomainEventHandler : INotificationHandler<CreatePaiementDomainEvent>
    {
        private readonly IPaiementRepository _paiementRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IEmailService _emailService;

        public CreatePaiementDomainEventHandler(
            IPaiementRepository paiementRepository,
            IEmailService emailService,
            IAcheteurRepository acheteurRepository)
        {
            _paiementRepository = paiementRepository;
            _emailService = emailService;
            _acheteurRepository = acheteurRepository;
        }

        public async Task Handle(CreatePaiementDomainEvent notification, CancellationToken cancellationToken)
        {
            var paiement = await _paiementRepository.GetByIdAsync(notification.PaiementID);

            if (paiement is null)
                return;

            Acheteur? acheteur = await _acheteurRepository.GetByIdAsync(notification.AcheteurID);

            if (acheteur is null)
            {
                return;
            }

            string email = acheteur.Email;

            string emailBody = _BuildEmailBody(paiement, paiement.PaiementDate.PlanDePaiement);

            await _emailService.SendEmailAsync(
                to: email,
                subject: "Confirmation de paiement – Merci pour votre règlement",
                body: emailBody);
        }

        private string _BuildEmailBody(Paiement paiement, PlanDePaiement plan)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bonjour,");
            sb.AppendLine();
            sb.AppendLine($"Nous avons le plaisir de vous informer que nous avons bien reçu votre paiement.");
            sb.AppendLine($"- Montant payé : {paiement.MontantPayee} TND");
            sb.AppendLine($"- Date du paiement : {paiement.DateDePaiement}");
            sb.AppendLine($"- Montant Total du Plan : {plan.MontantTotal} TND");
            sb.AppendLine($"- Montant Restant à Payer : {plan.MontantRestant} TND");

            sb.AppendLine();
            sb.AppendLine("Merci de respecter les échéances pour éviter des pénalités.");
            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }
    }
}
