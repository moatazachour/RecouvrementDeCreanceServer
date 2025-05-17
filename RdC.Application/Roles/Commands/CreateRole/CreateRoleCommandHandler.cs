using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;

namespace RdC.Application.Roles.Commands.CreateRole
{
    internal sealed class CreateRoleCommandHandler
        : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(
            IRoleRepository roleRepository, 
            IRolePermissionRepository rolePermissionRepository, 
            IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = Role.Create(request.addRoleRequest.roleName);

            await _roleRepository.AddAsync(role);

            await _unitOfWork.CommitChangesAsync();

            var rolePermissions = request.addRoleRequest.rolePermission.Select(rp => new RolePermission(
                                                            id: 0,
                                                            permissionDefinitionID: rp.permissionDefinitionID,
                                                            roleID: role.Id,
                                                            canRead: rp.canRead,
                                                            canWrite: rp.canWrite,
                                                            canCreate: rp.canCreate)).ToList();

            if (rolePermissions.Any())
            {
                await _rolePermissionRepository.AddRangeAsync(rolePermissions);
            }

            await _unitOfWork.CommitChangesAsync();

            return role.Id;
        }
    }
}
