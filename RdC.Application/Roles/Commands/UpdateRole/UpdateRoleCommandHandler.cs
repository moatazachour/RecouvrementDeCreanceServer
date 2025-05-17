using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;

namespace RdC.Application.Roles.Commands.UpdateRole
{
    internal sealed class UpdateRoleCommandHandler
        : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(
            IRoleRepository roleRepository, 
            IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.roleID);

            if (role is null)
            {
                return false;
            }

            role.RoleName = request.roleRequest.roleName;

            var existingRolePermissions = role.RolePermissions;
            var newRolePermissions = request.roleRequest.rolePermission;

            foreach ( var existingRolePermission in existingRolePermissions )
            {
                var updated = newRolePermissions
                    .FirstOrDefault(nrp => nrp.permissionDefinitionID == existingRolePermission.PermissionDefinitionID);

                if ( updated != null )
                {
                    existingRolePermission.CanRead = updated.canRead;
                    existingRolePermission.CanWrite = updated.canWrite;
                    existingRolePermission.CanCreate = updated.canCreate;
                }
            }

            await _unitOfWork.CommitChangesAsync();

            return true;
        }
    }
}
