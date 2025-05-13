using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Users.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public UserRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(r => r.PermissionDefinition)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int userID)
        {
            return await _dbContext.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(r => r.PermissionDefinition)
                .FirstOrDefaultAsync(u => u.Id == userID);
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
