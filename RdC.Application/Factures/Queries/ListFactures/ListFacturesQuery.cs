using MediatR;
using RdC.Domain.Factures;

namespace RdC.Application.Factures.Queries.ListFactures
{
    public record ListFacturesQuery() : IRequest<List<Facture>>;
}
