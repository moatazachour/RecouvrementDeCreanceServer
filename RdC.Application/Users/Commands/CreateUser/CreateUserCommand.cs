using MediatR;

namespace RdC.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string email,
        int roleID) 
        : IRequest<int>;
}
