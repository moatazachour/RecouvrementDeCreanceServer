using MediatR;
using RdC.Domain.DTO.Role;

namespace RdC.Application.Roles.Commands.CreateRole
{
    public record CreateRoleCommand(
        RoleRequest addRoleRequest)
        : IRequest<int>;
}
