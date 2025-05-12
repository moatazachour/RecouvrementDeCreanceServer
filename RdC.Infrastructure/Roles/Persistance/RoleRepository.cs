using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Roles.Persistance
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public RoleRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Role role)
        {
            await _dbContext.Roles.AddAsync(role);

            return true;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _dbContext.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.PermissionDefinition)
                .ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int roleID)
        {
            return await _dbContext.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.PermissionDefinition)
                .FirstOrDefaultAsync(r => r.Id == roleID);
        }
    }
}
