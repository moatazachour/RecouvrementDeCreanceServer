using RdC.Domain.Users;

namespace RdC.Application.Common.Interfaces
{
    public interface IRoleRepository
    {
        Task<bool> AddAsync(Role role);

        Task<Role?> GetByIdAsync(int roleID);

        Task<List<Role>> GetAllAsync();
    }
}
