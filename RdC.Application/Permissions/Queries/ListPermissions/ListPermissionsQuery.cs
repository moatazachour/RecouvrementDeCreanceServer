using MediatR;
using RdC.Domain.DTO.Role;

namespace RdC.Application.Permissions.Queries.ListPermissions
{
    public record ListPermissionsQuery() : IRequest<List<PermissionDefinitionResponse>>;
}
