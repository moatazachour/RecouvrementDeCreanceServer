using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Factures;

namespace RdC.Application.Factures.Queries.ListFactures
{
    public class ListFacturesQueryHandler : IRequestHandler<ListFacturesQuery, List<Facture>>
    {
        private readonly IFactureRepository _factureRepository;

        public ListFacturesQueryHandler(IFactureRepository factureRepository, IAcheteurRepository acheteurRepository)
        {
            _factureRepository = factureRepository;
        }

        public async Task<List<Facture>> Handle(ListFacturesQuery request, CancellationToken cancellationToken)
        {
            var factures = await _factureRepository.ListAsync();

            return factures;
        }
    }
}
