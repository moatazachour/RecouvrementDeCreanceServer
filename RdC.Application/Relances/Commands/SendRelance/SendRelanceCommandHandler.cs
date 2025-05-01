using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Domain.Relances;
using RdC.Domain.Relances.Events;
using System.Text;

namespace RdC.Application.Relances.Commands.SendRelance
{
    internal sealed class SendRelanceCommandHandler
        : IRequestHandler<SendRelanceCommand, bool>
    {
        private readonly IEmailRelanceRepository _emailRelanceRepository;
        private readonly ISMSRelanceRepository _smsRelanceRepository;
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public SendRelanceCommandHandler(
            IEmailRelanceRepository emailRelanceRepository,
            ISMSRelanceRepository smsRelanceRepository,
            IPaiementDateRepository paiementDateRepository,
            IAcheteurRepository acheteurRepository,
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher,
            IPlanDePaiementRepository planDePaiementRepository)
        {
            _emailRelanceRepository = emailRelanceRepository;
            _smsRelanceRepository = smsRelanceRepository;
            _paiementDateRepository = paiementDateRepository;
            _acheteurRepository = acheteurRepository;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
            _planDePaiementRepository = planDePaiementRepository;
        }

        public async Task<bool> Handle(SendRelanceCommand request, CancellationToken cancellationToken)
        {
            var currentPaiementDate = await _paiementDateRepository.GetByIdAsync(request.PaiementDateID);

            if (currentPaiementDate is null)
            {
                return false;
            }

            var plan = await _planDePaiementRepository.GetByIdAsync(currentPaiementDate.PlanID);

            if (plan is null)
            {
                return false;
            }

            var acheteur = await _acheteurRepository.GetByIdAsync(plan.Factures[0].AcheteurID);

            if (acheteur is null)
            {
                return false;
            }

            string subject = string.Empty;
            string body = string.Empty;

            if (request.RelanceContext == RelanceContext.UpcomingPaymentReminder)
            {
                subject = "Rappel de Paiement à Venir";
                body = _BuildUpcomingPaymentReminder(currentPaiementDate);
            }

            if (request.RelanceContext == RelanceContext.OverduePaymentReminder)
            {
                subject = "Rappel de Paiement en Retard";
                body = _BuildOverduePaymentReminder(currentPaiementDate);
            }

            if (request.RelanceContext == RelanceContext.UpcomingPaymentReminderWithUnpaidPreviousPayment)
            {
                subject = "Rappel de Paiement à Venir";
                body = await _BuildUpcomingPaymentReminderWithUnpaidPreviousPaiement(currentPaiementDate);
            }

            var emailRelance = EmailRelance.Send(
                currentPaiementDate.Id,
                acheteur.Email,
                body
                );

            var smsRelance = SMSRelance.Send(
                currentPaiementDate.Id,
                acheteur.Telephone,
                body);

            await _emailRelanceRepository.AddAsync(emailRelance);

            await _smsRelanceRepository.AddAsync(smsRelance);

            await _unitOfWork.CommitChangesAsync();

            currentPaiementDate.RaiseDomainEvent(new SendEmailDomainEvent(acheteur.Email, subject, body));

            await _domainEventDispatcher.DispatchEventsAsync(currentPaiementDate);

            return true;
        }

        private string _BuildUpcomingPaymentReminder(PaiementDate paiementDate)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Bonjour,");
            sb.AppendLine();
            sb.AppendLine($"Nous vous rappelons que l’échéance de paiement de {paiementDate.MontantDue} DNT approche.");
            sb.AppendLine($"Date d’échéance : {paiementDate.EcheanceDate:dd/MM/yyyy}.");
            sb.AppendLine();
            sb.AppendLine("Merci de prévoir le règlement dans les délais.");
            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");
            return sb.ToString();
        }

        private async Task<string> _BuildUpcomingPaymentReminderWithUnpaidPreviousPaiement(
            PaiementDate paiementDate)
        {
            var previousPaiementDate = await _paiementDateRepository.GetPreviousPaiementDateAsync(paiementDate.Id);

            var sb = new StringBuilder();
            sb.AppendLine("Bonjour,");
            sb.AppendLine();

            sb.AppendLine($"Nous vous rappelons que l’échéance de paiement de {paiementDate.MontantDue} DNT approche.");
            sb.AppendLine($"Date d’échéance : {paiementDate.EcheanceDate:dd/MM/yyyy}.");

            if (previousPaiementDate != null)
            {
                decimal montantTotal = previousPaiementDate.MontantDue + paiementDate.MontantDue;

                sb.AppendLine();
                sb.AppendLine($"⚠️ Nous constatons également qu’un paiement antérieur de {previousPaiementDate.MontantDue} DNT, prévu le {previousPaiementDate.EcheanceDate:dd/MM/yyyy}, n’a pas encore été réglé.");
                sb.AppendLine("Nous vous invitons à régulariser cette situation dans les plus brefs délais.");

                sb.AppendLine();
                sb.AppendLine($"💡 À noter : si le paiement de cette échéance précédente n’est pas effectué avant demain,");
                sb.AppendLine($"le montant total à régler demain sera de {montantTotal} DNT.");
            }

            sb.AppendLine();
            sb.AppendLine("Merci de votre compréhension.");
            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");

            return sb.ToString();
        }

        private string _BuildOverduePaymentReminder(PaiementDate paiementDate)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Bonjour,");
            sb.AppendLine();
            sb.AppendLine($"Nous vous informons que l’échéance de paiement prévue le {paiementDate.EcheanceDate:dd/MM/yyyy} est dépassée.");
            sb.AppendLine($"Montant en attente : {paiementDate.MontantDue} DNT.");
            sb.AppendLine();
            sb.AppendLine("Merci de procéder au paiement dans les plus brefs délais afin d’éviter toute pénalité.");
            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");
            return sb.ToString();
        }

    }
}
