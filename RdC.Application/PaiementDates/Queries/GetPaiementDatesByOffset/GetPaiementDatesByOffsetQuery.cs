using MediatR;
using RdC.Domain.PaiementDates;

namespace RdC.Application.PaiementDates.Queries.GetPaiementDatesByOffset
{
    public record GetPaiementDatesByOffsetQuery(int DaysOffset)
        : IRequest<List<PaiementDate>>;
}
