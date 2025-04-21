using MediatR;

namespace RdC.Application.Litiges.Commands.ResolveDuplicated
{
    public record ResolveDuplicatedCommand(int LitigeID) : IRequest<bool>;
}
