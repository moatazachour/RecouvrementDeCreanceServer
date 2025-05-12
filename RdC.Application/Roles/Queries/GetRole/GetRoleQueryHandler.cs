using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Role;

namespace RdC.Application.Roles.Queries.GetRole
{
    internal sealed class GetRoleQueryHandler
        : IRequestHandler<GetRoleQuery, RoleResponse?>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleResponse?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.roleID);

            if (role is null)
            {
                return null;
            }

            var rolePermissionsResponse = role.RolePermissions.Select(rp => new RolePermissionResponse(
                rp.Id,
                new PermissionDefinitionResponse(
                    rp.PermissionDefinition.Id,
                    rp.PermissionDefinition.PermissionName),
                rp.CanRead,
                rp.CanWrite,
                rp.CanCreate)).ToList();


            var roleResponse = new RoleResponse(
                                        role.Id, 
                                        role.RoleName, 
                                        rolePermissionsResponse);

            return roleResponse;
        }
    }
}
