using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Factures;
using System.Runtime.CompilerServices;

namespace RdC.Application.Factures.Queries.GetFacture
{
    public class GetFactureQueryHandler : IRequestHandler<GetFactureQuery, Facture?>
    {
        private readonly IFactureRepository _factureRepository;

        public GetFactureQueryHandler(IFactureRepository factureRepository)
        {
            _factureRepository = factureRepository;
        }

        public Task<Facture?> Handle(GetFactureQuery request, CancellationToken cancellationToken)
        {
            var facture = _factureRepository.GetByIdAsync(request.FactureID);

            return facture;
        }
    }
}
