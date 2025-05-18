using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;

namespace RdC.Application.PlanDePaiements.Commands.CheckPlanStatus
{
    internal sealed class CheckPlanStatusCommandHandler
        : IRequestHandler<CheckPlanStatusCommand, bool>
    {
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public CheckPlanStatusCommandHandler(
            IPlanDePaiementRepository planDePaiementRepository, 
            IPaiementDateRepository paiementDateRepository,
            IAcheteurRepository acheteurRepository,
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _paiementDateRepository = paiementDateRepository;
            _acheteurRepository = acheteurRepository;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<bool> Handle(CheckPlanStatusCommand request, CancellationToken cancellationToken)
        {
            var allPreviousPaiementDates = 
                await _paiementDateRepository.GetAllPreviousPaiementDateAsync(request.planID);
            
            var plan = await _planDePaiementRepository.GetByIdAsync(request.planID);
            
            var paiementDates = await _paiementDateRepository.GetByPlanIdAsync(request.planID);

            if (plan is null)
            {
                throw new KeyNotFoundException($"Plan de paiement with ID {request.planID} was not found.");
            }

            var acheteur = await _acheteurRepository.GetByIdAsync(plan.Factures[0].AcheteurID);

            if (_CheckIfPlanHaveMaxUnpaidPaiements(allPreviousPaiementDates, request.maxUpaidPaiements))
            {
                plan.Desactivate(request.maxUpaidPaiements);

                acheteur.Score -= (float)Penalties.PlanCancelationPenalty;

                paiementDates.ForEach(pd => pd.IsLocked = true);

                await _unitOfWork.CommitChangesAsync();

                await _domainEventDispatcher.DispatchEventsAsync(plan);

                return false;
            }

            return true;
        }

        private bool _CheckIfPlanHaveMaxUnpaidPaiements(List<PaiementDate> allPreviousPaiementDates, int maxUnpaidPaiements) // Change method name
        {
            int unpaidPaiementDateConsecutiveCounter = 0;

            foreach (PaiementDate unpaidPaiementDate in allPreviousPaiementDates)
            {
                unpaidPaiementDateConsecutiveCounter++;

                if (unpaidPaiementDate.IsPaid)
                {
                    unpaidPaiementDateConsecutiveCounter = 0;
                }
            }

            return unpaidPaiementDateConsecutiveCounter == maxUnpaidPaiements - 1;
        }
    }
}
