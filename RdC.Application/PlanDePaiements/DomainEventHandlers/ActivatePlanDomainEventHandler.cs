using MediatR;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
using RdC.Domain.PlanDePaiements;
using RdC.Domain.PlanDePaiements.Events;
using System.Text;

namespace RdC.Application.PlanDePaiements.DomainEventHandlers
{
    internal sealed class ActivatePlanDomainEventHandler
        : INotificationHandler<ActivatePlanDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IAcheteurRepository _acheteurRepository;

        public ActivatePlanDomainEventHandler(
            IEmailService emailService,
            IAcheteurRepository acheteurRepository)
        {
            _emailService = emailService;
            _acheteurRepository = acheteurRepository;
        }

        public async Task Handle(ActivatePlanDomainEvent notification, CancellationToken cancellationToken)
        {
            PlanDePaiement? plan = notification.PlanDePaiement;

            if (plan is null) return;

            Acheteur? acheteur = await _acheteurRepository.GetByIdAsync(plan.Factures[0].AcheteurID);

            if (acheteur is null) return;

            string acheteurEmail = acheteur.Email;

            string emailBody = _BuildEmailBody(plan);

            await _emailService.SendEmailAsync(
                to: acheteurEmail,
                subject: "Plan de paiement Activé",
                body: emailBody);
        }

        private string _BuildEmailBody(PlanDePaiement plan)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bonjour,");
            sb.AppendLine();
            sb.AppendLine("Votre plan de paiement est En Cours!");

            sb.AppendLine();
            sb.AppendLine("Les écheances:");
            sb.AppendLine();

            foreach (var paiementDate in plan.PaiementsDates)
            {
                sb.AppendLine($"{paiementDate.EcheanceDate} - {paiementDate.MontantDeEcheance} DNT");
            }

            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }
    }
}
