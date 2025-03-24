﻿using MediatR;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Domain.PaiementDates.Events;
using RdC.Domain.PlanDePaiements;
using System.Text;

namespace RdC.Application.PaiementDates.Events
{
    internal sealed class CreatePaiementDatesDomainEventHandler : INotificationHandler<CreatePaiementDatesDomainEvent>
    {
        private IPlanDePaiementRepository _planDePaiementRepository;
        private IPaiementDateRepository _paiementDateRepository;
        private IEmailService _emailService;

        public CreatePaiementDatesDomainEventHandler(
            IPlanDePaiementRepository planDePaiementRepository,
            IPaiementDateRepository paiementDateRepository,
            IEmailService emailService)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _paiementDateRepository = paiementDateRepository;
            _emailService = emailService;
        }

        public async Task Handle(CreatePaiementDatesDomainEvent notification, CancellationToken cancellationToken)
        {
            var plan = await _planDePaiementRepository.GetByIdAsync(notification.PlanID);

            if (plan is null)
                return;

            var paiementsDates = await _paiementDateRepository.GetByPlanIdAsync(notification.PlanID);

            string acheteurEmail = plan.Factures[0].Acheteur.Email;

            var emailBody = _BuildEmailBody(plan, paiementsDates);

            await _emailService.SendEmailAsync(
                to: acheteurEmail,
                subject: "Plan de paiement activée",
                body: emailBody);

        }

        private string _BuildEmailBody(PlanDePaiement plan, IEnumerable<PaiementDate> paiementDates)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bonjour,");
            sb.AppendLine();
            sb.AppendLine($"Votre plan de paiement a été créé avec les détails suivants :");
            sb.AppendLine($"- Montant Total : {plan.MontantTotal} €");
            sb.AppendLine($"- Nombre d'échéances : {plan.NombreDeEcheances}");
            sb.AppendLine();
            sb.AppendLine("Voici vos dates de paiement :");

            foreach (var paiement in paiementDates)
            {
                sb.AppendLine($"- {paiement.EcheanceDate.ToString("dd/MM/yyyy")}");
            }

            sb.AppendLine();
            sb.AppendLine("Merci de respecter les échéances pour éviter des pénalités.");
            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }
    }
}
