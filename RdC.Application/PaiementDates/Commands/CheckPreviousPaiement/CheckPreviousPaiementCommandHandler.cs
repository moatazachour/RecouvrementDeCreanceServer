using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;

namespace RdC.Application.PaiementDates.Commands.CheckPreviousPaiement
{
    internal sealed class CheckPreviousPaiementCommandHandler
        : IRequestHandler<CheckPreviousPaiementCommand, bool>
    {
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IPlanDePaiementRepository _plandePaiementRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckPreviousPaiementCommandHandler(
            IPaiementDateRepository paiementDateRepository,
            IAcheteurRepository plandePaiementRepository,
            IPlanDePaiementRepository planDePaiementRepository,
            IUnitOfWork unitOfWork)
        {
            _paiementDateRepository = paiementDateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CheckPreviousPaiementCommand request, CancellationToken cancellationToken)
        {
            var currentPaiementDate = await _paiementDateRepository.GetByIdAsync(request.PaiementDateID);
            var previousPaiementDate = await _paiementDateRepository.GetPreviousPaiementDateAsync(request.PaiementDateID);

            if (currentPaiementDate == null) return false;

            var plan = await _plandePaiementRepository.GetByIdAsync(currentPaiementDate.PlanID);

            if (plan is null) return false;

            var acheteur = await _acheteurRepository.GetByIdAsync(plan.Factures[0].AcheteurID);

            if (acheteur is null) return false;

            if (previousPaiementDate != null && !previousPaiementDate.IsPaid)
            {
                previousPaiementDate.IsLocked = true;
                currentPaiementDate.MontantDue += previousPaiementDate.MontantDue;

                acheteur.Score -= (float)Penalties.MissedPaiementPenalty;

                await _paiementDateRepository.UpdateAsync(currentPaiementDate);
                await _paiementDateRepository.UpdateAsync(previousPaiementDate);

                await _unitOfWork.CommitChangesAsync();
            }

            return true;
        }
    }
}
