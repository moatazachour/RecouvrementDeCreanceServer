using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Paiements;
using RdC.Domain.Paiements.Events;
using System.Runtime.InteropServices;

namespace RdC.Application.Paiements.Commands.CreatePaiement
{
    internal sealed class CreatePaiementCommandHandler : IRequestHandler<CreatePaiementCommand, int>
    {
        private readonly IPaiementRepository _paiementRepository;
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaiementCommandHandler(
            IPaiementRepository paiementRepository, 
            IPlanDePaiementRepository planDePaiementRepository, 
            IPaiementDateRepository paiementDateRepository,
            IDomainEventDispatcher domainEventDispatcher,
            IUnitOfWork unitOfWork)
        {
            _paiementRepository = paiementRepository;
            _planDePaiementRepository = planDePaiementRepository;
            _paiementDateRepository = paiementDateRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreatePaiementCommand request, CancellationToken cancellationToken)
        {
            var paiementDate = await _paiementDateRepository.GetByIdAsync(request.createPaiementRequest.PaiementDateID);
            var planDePaiement = await _planDePaiementRepository.GetByIdAsync(request.createPaiementRequest.PlanID);

            decimal montantPayee = request.createPaiementRequest.MontantPayee;

            planDePaiement.MontantRestant -= montantPayee;

            if (planDePaiement.MontantRestant == 0)
            {
                planDePaiement.PlanStatus = Domain.PlanDePaiements.PlanStatus.Termine;

                foreach (var facture in planDePaiement.Factures)
                {
                    facture.Status = Domain.Factures.FactureStatus.Payee;
                }
            }

            paiementDate.MontantPayee += request.createPaiementRequest.MontantPayee;

            if (paiementDate.MontantPayee == paiementDate.MontantDue)
            {
                paiementDate.IsLocked = true;
                paiementDate.IsPaid = true;
            }

            paiementDate.MontantDue -= request.createPaiementRequest.MontantPayee;

            var paiement = Paiement.CreatePaiement(
                planDePaiement.Id,
                montantPayee,
                DateTime.Now);

            await _paiementRepository.AddAsync(paiement);

            await _unitOfWork.CommitChangesAsync();

            paiement.RaiseDomainEvent(new CreatePaiementDomainEvent(paiement.Id, planDePaiement.Factures[0].AcheteurID));

            await _domainEventDispatcher.DispatchEventsAsync(paiement);

            return paiement.Id;
        }
    }
}
