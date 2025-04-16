using MediatR;

namespace RdC.Application.PaiementDates.Commands.CheckPreviousPaiement
{
    public record CheckPreviousPaiementCommand(int PaiementDateID)
        : IRequest<bool>;
}
