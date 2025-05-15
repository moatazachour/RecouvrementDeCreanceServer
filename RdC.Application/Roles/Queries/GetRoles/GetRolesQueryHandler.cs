using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Role;
using RdC.Domain.DTO.User;

namespace RdC.Application.Roles.Queries.GetRoles
{
    internal sealed class GetRolesQueryHandler
        : IRequestHandler<GetRolesQuery, List<RoleResponseWithUsers>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleResponseWithUsers>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();

            var rolesResponse = roles.Select(r => new RoleResponseWithUsers(
                r.Id,
                r.RoleName,
                r.RolePermissions.Select(rp => new RolePermissionResponse(
                    rp.Id,
                    new PermissionDefinitionResponse(rp.PermissionDefinition.Id, rp.PermissionDefinition.PermissionName),
                    rp.CanRead,
                    rp.CanWrite,
                    rp.CanCreate)).ToList(),
                r.Users.Select(u => new UserBasicResponse(
                    u.Id,
                    u.Username,
                    u.Email,
                    u.Status,
                    u.CreatedAt)).ToList())).ToList();

            return rolesResponse;
        }
    }
}
