using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;

namespace RdC.Application.Litiges.Commands.RejectLitige
{
    internal sealed class RejectLitigeCommandHandler
        : IRequestHandler<RejectLitigeCommand, bool>
    {
        private readonly ILitigeRepository _litigeRepository;
        private readonly IFactureRepository _factureRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectLitigeCommandHandler(
            ILitigeRepository litigeRepository, 
            IFactureRepository factureRepository,
            IAcheteurRepository acheteurRepository,
            IUnitOfWork unitOfWork)
        {
            _litigeRepository = litigeRepository;
            _factureRepository = factureRepository;
            _acheteurRepository = acheteurRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RejectLitigeCommand request, CancellationToken cancellationToken)
        {
            var litige = await _litigeRepository.GetByIdAsync(request.LitigeID);

            if (litige is null)
            {
                return false;
            }

            litige.Reject(request.ResolutedByUserID);

            var facture = await _factureRepository.GetByIdAsync(litige.FactureID);

            if (facture is null)
            {
                return false;
            }

            var acheteur = await _acheteurRepository.GetByIdAsync(facture.AcheteurID);

            if (acheteur is null)
            {
                return false;
            }

            acheteur.Score -= (float)Penalties.RejectedLitigePenalty;

            facture.CheckFactureStatus();

            await _unitOfWork.CommitChangesAsync();

            return true;
        }
    }
}
