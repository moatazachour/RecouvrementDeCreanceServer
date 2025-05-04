using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.Litiges.Commands.RejectLitige
{
    internal sealed class RejectLitigeCommandHandler
        : IRequestHandler<RejectLitigeCommand, bool>
    {
        private readonly ILitigeRepository _litigeRepository;
        private readonly IFactureRepository _factureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectLitigeCommandHandler(
            ILitigeRepository litigeRepository, 
            IFactureRepository factureRepository, 
            IUnitOfWork unitOfWork)
        {
            _litigeRepository = litigeRepository;
            _factureRepository = factureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RejectLitigeCommand request, CancellationToken cancellationToken)
        {
            var litige = await _litigeRepository.GetByIdAsync(request.LitigeID);

            if (litige is null)
            {
                return false;
            }

            litige.Reject();

            var facture = await _factureRepository.GetByIdAsync(litige.FactureID);

            if (facture is null)
            {
                return false;
            }

            facture.CheckFactureStatus();

            await _unitOfWork.CommitChangesAsync();

            return true;
        }
    }
}
