using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Factures;

namespace RdC.Application.Factures.Queries.ListFactures
{
    public class ListFacturesQueryHandler : IRequestHandler<ListFacturesQuery, List<Facture>>
    {
        private readonly IFactureRepository _factureRepository;
        private readonly IAcheteurRepository _acheteurRepository;

        public ListFacturesQueryHandler(IFactureRepository factureRepository, IAcheteurRepository acheteurRepository)
        {
            _factureRepository = factureRepository;
            _acheteurRepository = acheteurRepository;
        }

        public async Task<List<Facture>> Handle(ListFacturesQuery request, CancellationToken cancellationToken)
        {
            var acheteur = await _acheteurRepository.ListAsync();
            var factures = await _factureRepository.ListAsync();

            return factures;
        }
    }
}
