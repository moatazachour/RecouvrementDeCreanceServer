﻿using MediatR;
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
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaiementCommandHandler(
            IPaiementRepository paiementRepository, 
            IPaiementDateRepository paiementDateRepository,
            IDomainEventDispatcher domainEventDispatcher,
            IUnitOfWork unitOfWork,
            IPlanDePaiementRepository planDePaiementRepository)
        {
            _paiementRepository = paiementRepository;
            _paiementDateRepository = paiementDateRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _unitOfWork = unitOfWork;
            _planDePaiementRepository = planDePaiementRepository;
        }

        public async Task<int> Handle(CreatePaiementCommand request, CancellationToken cancellationToken)
        {
            var paiementDate = await _paiementDateRepository.GetByIdAsync(request.createPaiementRequest.PaiementDateID);
            var planDePaiement = await _planDePaiementRepository.GetByIdAsync(paiementDate.PlanDePaiement.Id);

            decimal montantPayee = request.createPaiementRequest.MontantPayee;

            planDePaiement.MontantRestant -= montantPayee;

            if (planDePaiement.MontantRestant == 0)
            {
                planDePaiement.PlanStatus = Domain.PlanDePaiements.PlanStatus.TERMINE;
                planDePaiement.IsLocked = true;

                foreach (var facture in planDePaiement.Factures)
                {
                    facture.Status = Domain.Factures.FactureStatus.PAYEE;
                }
            }

            if (!paiementDate.IsPaid)
            {
                paiementDate.MontantPayee += montantPayee;

                paiementDate.MontantDue -= montantPayee;
            }

            if (paiementDate.MontantDue == 0 && !paiementDate.IsPaid)
            {
                paiementDate.IsLocked = true;
                paiementDate.IsPaid = true;
            }


            var paiement = Paiement.CreatePaiement(
                paiementDate.Id,
                montantPayee,
                request.createPaiementRequest.PaidByUserID);

            await _paiementRepository.AddAsync(paiement);

            await _unitOfWork.CommitChangesAsync();

            paiement.RaiseDomainEvent(new CreatePaiementDomainEvent(paiement.Id, planDePaiement.Factures[0].AcheteurID));

            await _domainEventDispatcher.DispatchEventsAsync(paiement);

            return paiement.Id;
        }
    }
}
