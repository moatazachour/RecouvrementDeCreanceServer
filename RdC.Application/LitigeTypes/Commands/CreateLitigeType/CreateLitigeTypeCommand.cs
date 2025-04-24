using MediatR;

namespace RdC.Application.LitigeTypes.Commands.CreateLitigeType
{
    public record CreateLitigeTypeCommand(
        string LitigeTypeName,
        string LitigeTypeDescription)
        : IRequest<int>;
}
