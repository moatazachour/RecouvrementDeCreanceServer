using MediatR;
using RdC.Domain.Factures;

namespace RdC.Application.Factures.Queries.GetFacture
{
    public record GetFactureQuery(int FactureID) : IRequest<Facture?>;
}
