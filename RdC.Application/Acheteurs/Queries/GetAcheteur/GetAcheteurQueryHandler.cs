using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;

namespace RdC.Application.Acheteurs.Queries.GetAcheteur
{
    public class GetAcheteurQueryHandler : IRequestHandler<GetAcheteurQuery, Acheteur?>
    {
        private readonly IAcheteurRepository _acheteurRepository;

        public GetAcheteurQueryHandler(IAcheteurRepository acheteurRepository)
        {
            _acheteurRepository = acheteurRepository;
        }

        public async Task<Acheteur?> Handle(GetAcheteurQuery request, CancellationToken cancellationToken)
        {
            var acheteur = await _acheteurRepository.GetByIdAsync(request.acheteurID);

            return acheteur;
        }
    }
}
