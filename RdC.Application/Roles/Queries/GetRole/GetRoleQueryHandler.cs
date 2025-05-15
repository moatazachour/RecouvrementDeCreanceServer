using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Role;
using RdC.Domain.DTO.User;

namespace RdC.Application.Roles.Queries.GetRole
{
    internal sealed class GetRoleQueryHandler
        : IRequestHandler<GetRoleQuery, RoleResponseWithUsers?>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleResponseWithUsers?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
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


            var roleResponse = new RoleResponseWithUsers(
                                        role.Id,
                                        role.RoleName,
                                        rolePermissionsResponse,
                                        role.Users.Select(user => new UserBasicResponse(
                                            user.Id,
                                            user.Username,
                                            user.Email,
                                            user.Status,
                                            user.CreatedAt)).ToList());

            return roleResponse;
        }
    }
}
