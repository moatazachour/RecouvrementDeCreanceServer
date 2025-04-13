using MediatR;

namespace RdC.Application.Relances.Commands.SendRelance
{
    public record SendRelanceCommand(int PaiementDateID) : IRequest<bool>;
}
