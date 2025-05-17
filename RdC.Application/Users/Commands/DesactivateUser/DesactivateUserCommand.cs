using MediatR;
using RdC.Domain.Abstrations;

namespace RdC.Application.Users.Commands.DesactivateUser
{
    public record DesactivateUserCommand(int userID) : IRequest<Result<bool>>;
}
