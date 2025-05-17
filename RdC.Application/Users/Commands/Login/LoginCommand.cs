using MediatR;
using RdC.Domain.Abstrations;
using RdC.Domain.DTO.User;

namespace RdC.Application.Users.Commands.Login
{
    public record LoginCommand(
        string Identifier,
        string Password)
        : IRequest<Result<UserResponse>>;
}
