using MediatR;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;
using RdC.Domain.PlanDePaiements.Events;
using System.Text;

namespace RdC.Application.PlanDePaiements.DomainEventHandlers
{
    internal sealed class DesactivatePlanDomainEventHandler
        : INotificationHandler<DesactivatePlanDomainEvent>
    {
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IEmailService _emailService;
        private readonly IAcheteurRepository _acheteurRepository;

        public DesactivatePlanDomainEventHandler(
            IPlanDePaiementRepository planDePaiementRepository, 
            IEmailService emailService,
            IAcheteurRepository acheteurRepository)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _emailService = emailService;
            _acheteurRepository = acheteurRepository;
        }

        public async Task Handle(DesactivatePlanDomainEvent notification, CancellationToken cancellationToken)
        {
            var plan = await _planDePaiementRepository.GetByIdAsync(notification.PlanID);

            if (plan is null) 
                return;

            var acheteur = await _acheteurRepository.GetByIdAsync(plan.Factures[0].AcheteurID);

            if (acheteur is null) 
                return;

            string acheteurEmail = acheteur.Email;

            string emailBody = _BuildEmailBody(plan, notification.missedPaiementsCount);

            await _emailService.SendEmailAsync(
                to: acheteurEmail,
                subject: "Desactivation du plan de paiement",
                body: emailBody);
        }

        private string _BuildEmailBody(PlanDePaiement plan, int numberOfMissedPaiements)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bonjour,");
            sb.AppendLine();
            sb.AppendLine(@$"Votre plan de paiement est Annullé due aux retard de payer {numberOfMissedPaiements} écheance consecutive!");
            sb.AppendLine($"Vous devez creer un nouveau plan!");

            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }

    }
}
