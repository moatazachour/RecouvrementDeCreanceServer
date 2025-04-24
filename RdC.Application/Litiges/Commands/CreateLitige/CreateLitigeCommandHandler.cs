using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Factures;
using RdC.Domain.Litiges;

namespace RdC.Application.Litiges.Commands.CreateLitige
{
    internal sealed class CreateLitigeCommandHandler
        : IRequestHandler<CreateLitigeCommand, int>
    {
        private readonly ILitigeRepository _litigeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFactureRepository _factureRepository;

        public CreateLitigeCommandHandler(
            ILitigeRepository litigeRepository, 
            IUnitOfWork unitOfWork,
            IFactureRepository factureRepository)
        {
            _litigeRepository = litigeRepository;
            _unitOfWork = unitOfWork;
            _factureRepository = factureRepository;
        }

        public async Task<int> Handle(CreateLitigeCommand request, CancellationToken cancellationToken)
        {
            var litige = Litige.Declare(
                request.createLitigeRequest.FactureID,
                request.createLitigeRequest.TypeID,
                request.createLitigeRequest.LitigeDescription);

            var facture = await _factureRepository.GetByIdAsync(litige.FactureID);

            await _litigeRepository.AddAsync(litige);

            facture.Status = FactureStatus.EN_LITIGE;

            await _unitOfWork.CommitChangesAsync();

            return litige.Id;
        }
    }
}
