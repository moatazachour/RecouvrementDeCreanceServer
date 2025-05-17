using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.Litiges.Commands.ResolveAmountError
{
    internal sealed class ResolveAmountErrorCommandHandler
        : IRequestHandler<ResolveAmountErrorCommand, bool>
    {
        private readonly ILitigeRepository _litigeRepository;
        private readonly IFactureRepository _factureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResolveAmountErrorCommandHandler(
            ILitigeRepository litigeRepository, 
            IFactureRepository factureRepository, 
            IUnitOfWork unitOfWork)
        {
            _litigeRepository = litigeRepository;
            _factureRepository = factureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ResolveAmountErrorCommand request, CancellationToken cancellationToken)
        {
            var litige = await _litigeRepository.GetByIdAsync(request.LitigeID);

            if (litige is null)
            {
                return false;
            }

            litige.Accept(request.ResolutedByUserID);

            var facture = await _factureRepository.GetByIdAsync(litige.FactureID);

            if (facture is null)
            {
                return false;
            }

            facture.CorrectFactureAmounts(
                request.CorrectedTotalAmount,
                request.CorrectedAmountDue);

            facture.GetFactureStatus();

            await _unitOfWork.CommitChangesAsync();

            return true;
        }
    }
}
