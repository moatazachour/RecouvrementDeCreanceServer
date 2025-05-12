using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Role;

namespace RdC.Application.Roles.Queries.GetRoles
{
    internal sealed class GetRolesQueryHandler
        : IRequestHandler<GetRolesQuery, List<RoleResponse>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();

            var rolesResponse = roles.Select(r => new RoleResponse(
                r.Id,
                r.RoleName,
                r.RolePermissions.Select(rp => new RolePermissionResponse(
                    rp.Id,
                    new PermissionDefinitionResponse(rp.PermissionDefinition.Id, rp.PermissionDefinition.PermissionName),
                    rp.CanRead,
                    rp.CanWrite,
                    rp.CanCreate)).ToList())).ToList();

            return rolesResponse;
        }
    }
}
