using MediatR;
using RdC.Domain.DTO.LitigeType;

namespace RdC.Application.LitigeTypes.Commands.UpdateLitigeType
{
    public record UpdateLitigeTypeCommand(
        int LitigeTypeID,
        string LitigeTypeName,
        string LitigeTypeDescription)
        : IRequest<LitigeTypeResponse?>;
}
