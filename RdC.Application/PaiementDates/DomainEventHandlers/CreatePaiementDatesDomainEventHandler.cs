using MediatR;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Application.Common.Pdf;
using RdC.Domain.PaiementDates;
using RdC.Domain.PaiementDates.Events;
using RdC.Domain.PlanDePaiements;
using System.Text;

namespace RdC.Application.PaiementDates.DomainEventHandlers
{
    internal sealed class CreatePaiementDatesDomainEventHandler : INotificationHandler<CreatePaiementDatesDomainEvent>
    {
        private IPlanDePaiementRepository _planDePaiementRepository;
        private IPaiementDateRepository _paiementDateRepository;
        private IEmailService _emailService;
        private IAcheteurRepository _acheteurRepository;
        private IPdfGeneratorService _pdfGeneratorService;

        public CreatePaiementDatesDomainEventHandler(
            IPlanDePaiementRepository planDePaiementRepository,
            IPaiementDateRepository paiementDateRepository,
            IEmailService emailService,
            IAcheteurRepository acheteurRepository,
            IPdfGeneratorService pdfGeneratorService)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _paiementDateRepository = paiementDateRepository;
            _emailService = emailService;
            _acheteurRepository = acheteurRepository;
            _pdfGeneratorService = pdfGeneratorService;
        }

        public async Task Handle(CreatePaiementDatesDomainEvent notification, CancellationToken cancellationToken)
        {
            var plan = await _planDePaiementRepository.GetByIdAsync(notification.PlanID);

            if (plan is null)
                return;

            var paiementsDates = await _paiementDateRepository.GetByPlanIdAsync(notification.PlanID);

            var acheteur = await _acheteurRepository.GetByIdAsync(plan.Factures[0].AcheteurID);

            if (acheteur is null)
                return;

            string acheteurEmail = acheteur.Email;

            string emailBody = _BuildEmailBody(plan, paiementsDates);

            byte[] attachmentBytes = _pdfGeneratorService.GeneratePlanDePaiementPdf(plan, paiementsDates, acheteur);

            await _emailService.SendEmailWithAttachmentAsync(
                to: acheteurEmail,
                subject: "Votre Plan de Paiement",
                body: emailBody,
                attachmentBytes: attachmentBytes,
                attachmentFileName: "PlanDePaiement.pdf");

        }

        private string _BuildEmailBody(PlanDePaiement plan, IEnumerable<PaiementDate> paiementDates)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bonjour,");
            sb.AppendLine();
            sb.AppendLine($"Veuillez trouver ci-joint votre plan de paiement.");
            sb.AppendLine($"Merci de bien vouloir signer le document afin de confirmer votre accord sur ce plan.");

            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }
    }
}
