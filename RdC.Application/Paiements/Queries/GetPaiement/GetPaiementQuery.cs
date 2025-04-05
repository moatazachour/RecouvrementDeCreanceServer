using MediatR;
using RdC.Domain.DTO.Paiement;

namespace RdC.Application.Paiements.Queries.GetPaiement
{
    public record GetPaiementQuery(int PaiementID) : IRequest<PaiementResponse?>;
}
