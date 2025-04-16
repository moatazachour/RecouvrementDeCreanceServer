using MediatR;
using RdC.Domain.DTO.PaiementDate;

namespace RdC.Application.PaiementDates.Commands.CreatePaiementDates
{
    public record CreatePaiementDatesCommand(
        CreatePaiementDatesRequest createPaiementDatesRequest)
        : IRequest<Unit>;
}
