using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Paiement;

namespace RdC.Application.Paiements.Queries.GetPaiement
{
    internal sealed class GetPaiementQueryHandler : IRequestHandler<GetPaiementQuery, PaiementResponse?>
    {
        private readonly IPaiementRepository _paiementRepository;

        public GetPaiementQueryHandler(IPaiementRepository paiementRepository)
        {
            _paiementRepository = paiementRepository;
        }

        public async Task<PaiementResponse?> Handle(GetPaiementQuery request, CancellationToken cancellationToken)
        {
            var paiement = await _paiementRepository.GetByIdAsync(request.PaiementID);

            if (paiement is null)
                return null;

            return new PaiementResponse(
                paiement.Id,
                paiement.PaiementDateID,
                paiement.MontantPayee,
                paiement.DateDePaiement);
        }
    }
}
