using MediatR;
using RdC.Domain.DTO.Role;

namespace RdC.Application.Roles.Queries.GetRoles
{
    public record GetRolesQuery() : IRequest<List<RoleResponse>>;
}
