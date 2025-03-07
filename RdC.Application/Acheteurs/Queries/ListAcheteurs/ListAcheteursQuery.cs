using MediatR;
using RdC.Domain.Acheteurs;

namespace RdC.Application.Acheteurs.Queries.ListAcheteurs
{
    public record ListAcheteursQuery() : IRequest<List<Acheteur>>;
}
