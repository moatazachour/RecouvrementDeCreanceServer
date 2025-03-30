using MediatR;
using RdC.Domain.DTO.Paiement;

namespace RdC.Application.Paiements.Commands.CreatePaiement
{
    public record CreatePaiementCommand(
        CreatePaiementRequest createPaiementRequest) : IRequest<int>;
}
