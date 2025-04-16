using MediatR;
using RdC.Domain.PaiementDates;

namespace RdC.Application.PaiementDates.Queries.GetPreviousPaiementDate
{
    public record GetPreviousPaiementDateQuery(int paiementDateID)
        : IRequest<PaiementDate?>;

}
