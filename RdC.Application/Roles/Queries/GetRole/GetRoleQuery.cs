using MediatR;
using RdC.Domain.DTO.Role;

namespace RdC.Application.Roles.Queries.GetRole
{
    public record GetRoleQuery(int roleID) : IRequest<RoleResponse?>;
}
