using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Factures;

namespace RdC.Application.Factures.Commands.UpdateFacture
{
    public class UpdateFactureCommandHandler : IRequestHandler<UpdateFactureCommand, Facture?>
    {
        private readonly IFactureRepository _factureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFactureCommandHandler(IFactureRepository factureRepository, IUnitOfWork unitOfWork)
        {
            _factureRepository = factureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Facture?> Handle(UpdateFactureCommand request, CancellationToken cancellationToken)
        {
            var facture = await _factureRepository.UpdateAsync(request.FactureID, request.factureUpdate);
            await _unitOfWork.CommitChangesAsync();

            return facture;
        }
    }
}
