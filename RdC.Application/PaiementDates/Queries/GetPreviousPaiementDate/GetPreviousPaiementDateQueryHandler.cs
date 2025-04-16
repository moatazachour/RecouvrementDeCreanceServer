using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;

namespace RdC.Application.PaiementDates.Queries.GetPreviousPaiementDate
{
    internal sealed class GetPreviousPaiementDateQueryHandler
        : IRequestHandler<GetPreviousPaiementDateQuery, PaiementDate?>
    {
        private readonly IPaiementDateRepository _paiementDateRepository;

        public GetPreviousPaiementDateQueryHandler(IPaiementDateRepository paiementDateRepository)
        {
            _paiementDateRepository = paiementDateRepository;
        }

        public async Task<PaiementDate?> Handle(GetPreviousPaiementDateQuery request, CancellationToken cancellationToken)
        {
            return await _paiementDateRepository.GetPreviousPaiementDateAsync(request.paiementDateID);
        }
    }
}
