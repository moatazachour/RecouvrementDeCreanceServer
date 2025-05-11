using MediatR;
using RdC.Domain.DTO.Permission;

namespace RdC.Application.Permissions.Queries.ListPermissions
{
    public record ListPermissionsQuery() : IRequest<List<PermissionDefinitionResponse>>;
}
