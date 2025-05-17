using MediatR;
using RdC.Domain.Abstrations;

namespace RdC.Application.Users.Commands.ReactivateUser
{
    public record ReactivateUserCommand(int userID) : IRequest<Result<bool>>;
}
