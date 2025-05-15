using RdC.Domain.Litiges;

namespace RdC.Application.Common.Interfaces
{
    public interface ILitigeRepository
    {
        Task<bool> AddAsync(Litige litige);

        Task<Litige?> GetByIdAsync(int litigeID);

        Task<List<Litige>> GetAllDeclaredByUserIdAsync(int userId);

        Task<List<Litige>> GetAllResolutedByUserIdAsync(int userId);

        Task<List<Litige>> GetAllAsync();
    }
}
