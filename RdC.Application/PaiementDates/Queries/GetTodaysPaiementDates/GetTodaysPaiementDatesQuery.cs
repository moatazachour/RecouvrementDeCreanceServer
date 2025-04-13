using MediatR;
using RdC.Domain.PaiementDates;

namespace RdC.Application.PaiementDates.Queries.GetTodaysPaiementDates
{
    public record GetTodaysPaiementDatesQuery() : IRequest<List<PaiementDate>>;
}
