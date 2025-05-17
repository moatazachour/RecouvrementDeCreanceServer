using MediatR;

namespace RdC.Application.Litiges.Commands.ResolveDuplicated
{
    public record ResolveDuplicatedCommand(
        int LitigeID,
        int ResolutedByUserID) : IRequest<bool>;
}
