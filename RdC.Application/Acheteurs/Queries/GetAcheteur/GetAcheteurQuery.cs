using MediatR;
using RdC.Domain.Acheteurs;

namespace RdC.Application.Acheteurs.Queries.GetAcheteur
{
    public record GetAcheteurQuery(int acheteurID) : IRequest<Acheteur?>;
}
