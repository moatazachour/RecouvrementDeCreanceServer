using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.PlanDePaiements.Commands.LockPlan
{
    internal sealed class LockPlanCommandHandler
        : IRequestHandler<LockPlanCommand, bool>
    {
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LockPlanCommandHandler(
            IPlanDePaiementRepository planDePaiementRepository,
            IUnitOfWork unitOfWork)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(LockPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planDePaiementRepository.GetByIdAsync(request.PlanID);

            if (plan == null) 
                return false;

            plan.IsLocked = true;

            await _unitOfWork.CommitChangesAsync();

            return true;
        }
    }
}
