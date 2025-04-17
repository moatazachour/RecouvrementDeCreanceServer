using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.PaiementDates.Commands.CheckPreviousPaiement
{
    internal sealed class CheckPreviousPaiementCommandHandler
        : IRequestHandler<CheckPreviousPaiementCommand, bool>
    {
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckPreviousPaiementCommandHandler(
            IPaiementDateRepository paiementDateRepository, 
            IUnitOfWork unitOfWork)
        {
            _paiementDateRepository = paiementDateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CheckPreviousPaiementCommand request, CancellationToken cancellationToken)
        {
            var currentPaiementDate = await _paiementDateRepository.GetByIdAsync(request.PaiementDateID);
            var previousPaiementDate = await _paiementDateRepository.GetPreviousPaiementDateAsync(request.PaiementDateID);

            if (currentPaiementDate == null)
            {
                return false;
            }

            if (previousPaiementDate != null && !previousPaiementDate.IsPaid)
            {
                previousPaiementDate.IsLocked = true;
                currentPaiementDate.MontantDue += previousPaiementDate.MontantDue;

                await _paiementDateRepository.UpdateAsync(currentPaiementDate);
                await _paiementDateRepository.UpdateAsync(previousPaiementDate);

                await _unitOfWork.CommitChangesAsync();
            }

            return true;
        }
    }
}
