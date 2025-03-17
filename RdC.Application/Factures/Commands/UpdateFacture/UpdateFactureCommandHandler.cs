using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Factures;

namespace RdC.Application.Factures.Commands.UpdateFacture
{
    public class UpdateFactureCommandHandler : IRequestHandler<UpdateFactureCommand, Facture?>
    {
        private readonly IFactureRepository _factureRepository;

        public UpdateFactureCommandHandler(IFactureRepository factureRepository)
        {
            _factureRepository = factureRepository;
        }

        public async Task<Facture?> Handle(UpdateFactureCommand request, CancellationToken cancellationToken)
        {
            return await _factureRepository.UpdateAsync(request.FactureID, request.factureUpdate);
        }
    }
}
