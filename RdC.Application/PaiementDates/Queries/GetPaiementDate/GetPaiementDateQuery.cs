using MediatR;
using RdC.Domain.DTO.PaiementDate;

namespace RdC.Application.PaiementDates.Queries.GetPaiementDate
{
    public record GetPaiementDateQuery(int PaiementDateID) : IRequest<PaiementDateResponse?>;
}
