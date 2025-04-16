using MediatR;
using RdC.Domain.Relances;

namespace RdC.Application.Relances.Commands.SendRelance
{
    public record SendRelanceCommand(
        int PaiementDateID, 
        RelanceContext RelanceContext) 
        : IRequest<bool>;
}
