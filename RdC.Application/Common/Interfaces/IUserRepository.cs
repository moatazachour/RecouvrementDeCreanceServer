using RdC.Domain.Users;

namespace RdC.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);

        Task<User?> GetByIdAsync(int userID);

        Task<User?> GetByEmailAsync(string email);
        
        Task<User?> GetByIdentifier(string identifier);

        Task<List<User>> GetAllAsync();

        Task<bool> IsEmailExistAsync(string email);

        Task<bool> IsUsernameExistAsync(string username);
    }
}
