using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;
using RdC.Domain.Relances;
using RdC.Domain.Relances.Events;
using System.Text;

namespace RdC.Application.Relances.Commands.SendRelance
{
    internal sealed class SendRelanceCommandHandler
        : IRequestHandler<SendRelanceCommand, bool>
    {
        private readonly IEmailRelanceRepository _emailRelanceRepository;
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public SendRelanceCommandHandler(
            IEmailRelanceRepository emailRelanceRepository, 
            IPaiementDateRepository paiementDateRepository,
            IAcheteurRepository acheteurRepository,
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher,
            IPlanDePaiementRepository planDePaiementRepository)
        {
            _emailRelanceRepository = emailRelanceRepository;
            _paiementDateRepository = paiementDateRepository;
            _acheteurRepository = acheteurRepository;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
            _planDePaiementRepository = planDePaiementRepository;
        }

        public async Task<bool> Handle(SendRelanceCommand request, CancellationToken cancellationToken)
        {
            var currentPaiementDate = await _paiementDateRepository.GetByIdAsync(request.PaiementDateID);
            var previousPaiementDate = await _paiementDateRepository.GetPreviousPaiementDateAsync(request.PaiementDateID);

            if (currentPaiementDate is null)
            {
                return false;
            }

            var plan = await _planDePaiementRepository.GetByIdAsync(currentPaiementDate.PlanID);

            if (plan is null) 
                return false;

            var acheteur = await _acheteurRepository.GetByIdAsync(plan.Factures[0].AcheteurID);

            if (acheteur is null)
            {
                return false;
            }

            if (previousPaiementDate != null && !previousPaiementDate.IsPaid)
            {
                previousPaiementDate.IsLocked = true;

                currentPaiementDate.MontantDue += previousPaiementDate.MontantDue;

                await _paiementDateRepository.UpdateAsync(previousPaiementDate);
                await _paiementDateRepository.UpdateAsync(currentPaiementDate);
            }

            string emailBody = _BuildEmailBody(currentPaiementDate);

            var emailRelance = EmailRelance.Send(
                currentPaiementDate.Id,
                acheteur.Email,
                emailBody
                );

            await _emailRelanceRepository.AddAsync(emailRelance);

            await _unitOfWork.CommitChangesAsync();

            currentPaiementDate.RaiseDomainEvent(new SendEmailDomainEvent(acheteur.Email, emailBody));

            await _domainEventDispatcher.DispatchEventsAsync(currentPaiementDate);

            return true;
        }

        private string _BuildEmailBody(PaiementDate paiementDate)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bonjour,");
            sb.AppendLine();
            sb.AppendLine($"Nous vous rappelons que l’échéance de paiement prévue le {paiementDate.EcheanceDate:dd/MM/yyyy} est arrivée à terme.");
            sb.AppendLine($"Montant dû : {paiementDate.MontantDue} DNT.");
            sb.AppendLine();
            sb.AppendLine("Merci de procéder au paiement dans les plus brefs délais.");
            sb.AppendLine();
            sb.AppendLine("Cordialement,");
            sb.AppendLine("Votre équipe de gestion");
            return sb.ToString();
        }
    }
}
