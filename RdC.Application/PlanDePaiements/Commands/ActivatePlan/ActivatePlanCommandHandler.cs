using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.PlanDePaiements.Commands.ActivatePlan
{
    internal sealed class ActivatePlanCommandHandler
        : IRequestHandler<ActivatePlanCommand, bool>
    {
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public ActivatePlanCommandHandler(
            IPlanDePaiementRepository planDePaiementRepository, 
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<bool> Handle(ActivatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planDePaiementRepository.GetByIdAsync(request.planID);

            if (plan is null)
            {
                return false;
            }

            plan.Activate(request.ValidatedByUserID);

            await _unitOfWork.CommitChangesAsync();

            await _domainEventDispatcher.DispatchEventsAsync(plan);

            return true;
        }
    }
}
