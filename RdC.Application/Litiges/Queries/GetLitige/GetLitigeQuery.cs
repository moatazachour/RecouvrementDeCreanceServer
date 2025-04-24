using MediatR;
using RdC.Domain.DTO.Litige;

namespace RdC.Application.Litiges.Queries.GetLitige
{
    public record GetLitigeQuery(int litigeID) : IRequest<LitigeResponse?>;
}
