using RdC.Domain.Users;

namespace RdC.Application.Common.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<bool> AddRangeAsync(List<RolePermission> rolePermissions);
    }
}
