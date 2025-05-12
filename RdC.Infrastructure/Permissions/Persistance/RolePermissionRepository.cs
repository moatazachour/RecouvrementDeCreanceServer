using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Permissions.Persistance
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public RolePermissionRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddRangeAsync(List<RolePermission> rolePermissions)
        {
            await _dbContext.RolePermissions.AddRangeAsync(rolePermissions);

            return true;
        }
    }
}
