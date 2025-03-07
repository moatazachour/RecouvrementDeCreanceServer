using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;

namespace RdC.Application.Acheteurs.Queries.ListAcheteurs
{
    public class ListAcheteursQueryHandler : IRequestHandler<ListAcheteursQuery, List<Acheteur>>
    {
        private readonly IAcheteurRepository _acheteurRepository;

        public ListAcheteursQueryHandler(IAcheteurRepository acheteurRepository)
        {
            _acheteurRepository = acheteurRepository;
        }

        public async Task<List<Acheteur>> Handle(ListAcheteursQuery request, CancellationToken cancellationToken)
        {
            var acheteurs = await _acheteurRepository.ListAsync();

            return acheteurs;
        }
    }
}
