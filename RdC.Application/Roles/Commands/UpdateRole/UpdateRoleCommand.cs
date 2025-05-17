using MediatR;
using RdC.Domain.DTO.Role;

namespace RdC.Application.Roles.Commands.UpdateRole
{
    public record UpdateRoleCommand(
        int roleID,
        RoleRequest roleRequest)
        : IRequest<bool>;
}
