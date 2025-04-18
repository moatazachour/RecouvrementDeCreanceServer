using MediatR;
using RdC.Domain.DTO.LitigeType;

namespace RdC.Application.LitigeTypes.Queries.GetAllLitigeTypes
{
    public record GetAllLitigeTypesQuery() : IRequest<List<LitigeTypeResponse>>;
}
