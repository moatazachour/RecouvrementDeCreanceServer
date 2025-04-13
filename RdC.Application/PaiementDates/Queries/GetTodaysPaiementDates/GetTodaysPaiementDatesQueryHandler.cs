using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;

namespace RdC.Application.PaiementDates.Queries.GetTodaysPaiementDates
{
    internal sealed class GetTodaysPaiementDatesQueryHandler 
        : IRequestHandler<GetTodaysPaiementDatesQuery, List<PaiementDate>>
    {
        private readonly IPaiementDateRepository _paiementDateRepository;

        public GetTodaysPaiementDatesQueryHandler(
            IPaiementDateRepository paiementDateRepository)
        {
            _paiementDateRepository = paiementDateRepository;
        }

        public async Task<List<PaiementDate>> Handle(GetTodaysPaiementDatesQuery request, CancellationToken cancellationToken)
        {
            var todayPaiementDates = await _paiementDateRepository.GetTodaysAsync();

            return todayPaiementDates;
        }
    }
}
