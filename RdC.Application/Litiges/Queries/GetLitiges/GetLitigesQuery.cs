using MediatR;
using RdC.Domain.DTO.Litige;

namespace RdC.Application.Litiges.Queries.GetLitiges
{
    public record GetLitigesQuery() : IRequest<List<LitigeResponse>>;
}
