using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;

namespace RdC.Application.PaiementDates.Queries.GetPaiementDatesByOffset
{
    internal sealed class GetPaiementDatesByOffsetQueryHandler
        : IRequestHandler<GetPaiementDatesByOffsetQuery, List<PaiementDate>>
    {
        private readonly IPaiementDateRepository _paiementDateRepository;

        public GetPaiementDatesByOffsetQueryHandler(IPaiementDateRepository paiementDateRepository)
        {
            _paiementDateRepository = paiementDateRepository;
        }

        public async Task<List<PaiementDate>> Handle(GetPaiementDatesByOffsetQuery request, CancellationToken cancellationToken)
        {
            return await _paiementDateRepository.GetPaiementDatesByOffsetAsync(request.DaysOffset);
        }
    }
}
