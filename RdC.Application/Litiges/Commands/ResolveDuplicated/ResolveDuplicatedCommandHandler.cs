using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
using RdC.Domain.Factures;

namespace RdC.Application.Litiges.Commands.ResolveDuplicated
{
    internal sealed class ResolveDuplicatedCommandHandler
        : IRequestHandler<ResolveDuplicatedCommand, bool>
    {
        private readonly ILitigeRepository _litigeRepository;
        private readonly IFactureRepository _factureRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResolveDuplicatedCommandHandler(
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

        public async Task<bool> Handle(ResolveDuplicatedCommand request, CancellationToken cancellationToken)
        {
            bool isAccepted = false;

            var litige = await _litigeRepository.GetByIdAsync(request.LitigeID);

            if (litige is null)
            {
                throw new Exception($"Litige with ID {request.LitigeID} not found!");
            }

            var factures = await _factureRepository.ListAsync();

            var currentFacture = await _factureRepository.GetByIdAsync(litige.FactureID);

            if (currentFacture is null)
            {
                throw new Exception($"Facture with ID {litige.FactureID} not found!");
            }

            var acheteur = await _acheteurRepository.GetByIdAsync(currentFacture.AcheteurID);

            if (IsDuplicated(currentFacture, factures))
            {
                currentFacture.Status = FactureStatus.DUPLIQUE;

                litige.Accept(request.ResolutedByUserID);

                isAccepted = true;
            }
            else
            {
                litige.Reject(request.ResolutedByUserID);

                acheteur.Score -= (float)Penalties.RejectedLitigePenalty;

                currentFacture.CheckFactureStatus();

                isAccepted = false;
            }


            await _unitOfWork.CommitChangesAsync();

            return isAccepted;

        }

        private bool IsDuplicated(Facture currentFacture, List<Facture> factures)
        {
            int counter = 0;

            foreach (var facture in factures)
            {
                if (facture.Status != FactureStatus.DUPLIQUE && facture.NumFacture == currentFacture.NumFacture)
                    counter++;
            }

            return counter > 1;
        }
    }
}
