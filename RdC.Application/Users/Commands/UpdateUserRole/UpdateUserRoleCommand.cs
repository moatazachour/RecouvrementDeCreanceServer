using MediatR;

namespace RdC.Application.Users.Commands.UpdateUserRole
{
    public record UpdateUserRoleCommand(
        int userID,
        int roleID) 
        : IRequest<bool>;
}
