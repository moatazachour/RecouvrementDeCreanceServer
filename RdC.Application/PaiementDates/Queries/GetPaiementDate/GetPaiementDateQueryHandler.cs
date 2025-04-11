using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.DTO.PaiementDate;

namespace RdC.Application.PaiementDates.Queries.GetPaiementDate
{
    internal sealed class GetPaiementDateQueryHandler
        : IRequestHandler<GetPaiementDateQuery, PaiementDateResponse?>
    {
        private readonly IPaiementDateRepository _paiementDateRepository;

        public GetPaiementDateQueryHandler(IPaiementDateRepository paiementDateRepository)
        {
            _paiementDateRepository = paiementDateRepository;
        }

        public async Task<PaiementDateResponse?> Handle(GetPaiementDateQuery request, CancellationToken cancellationToken)
        {
            var paiementDate = await _paiementDateRepository.GetByIdAsync(request.PaiementDateID);

            if (paiementDate == null)
                return null;

            var paiementList = paiementDate.Paiements.Select(paiement => new PaiementResponse(
                                                        paiement.Id,
                                                        paiement.PaiementDateID,
                                                        paiement.MontantPayee,
                                                        paiement.DateDePaiement)).ToList();

            return new PaiementDateResponse(
                paiementDate.Id,
                paiementDate.PlanID,
                paiementDate.EcheanceDate,
                paiementDate.MontantDeEcheance,
                paiementDate.MontantPayee,
                paiementDate.MontantDue,
                paiementDate.IsPaid,
                paiementDate.IsLocked,
                paiementList);
        }
    }
}
