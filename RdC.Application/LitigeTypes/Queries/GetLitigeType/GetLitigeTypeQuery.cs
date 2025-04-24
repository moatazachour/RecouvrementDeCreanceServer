using MediatR;
using RdC.Domain.DTO.LitigeType;

namespace RdC.Application.LitigeTypes.Queries.GetLitigeType
{
    public record GetLitigeTypeQuery(int LitigeTypeID) : IRequest<LitigeTypeResponse?>;
}
