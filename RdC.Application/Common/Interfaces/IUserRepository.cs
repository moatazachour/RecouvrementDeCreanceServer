using RdC.Domain.Users;

namespace RdC.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);

        Task<User?> GetByIdAsync(int userID);

        Task<List<User>> GetAllAsync();

        Task<bool> IsEmailExistAsync(string email);
    }
}
