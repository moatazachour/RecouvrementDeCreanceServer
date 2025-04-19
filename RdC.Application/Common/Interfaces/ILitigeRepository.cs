using RdC.Domain.Litiges;

namespace RdC.Application.Common.Interfaces
{
    public interface ILitigeRepository
    {
        Task<bool> AddAsync(Litige litige);

        Task<Litige?> GetByIdAsync(int litigeID);

        Task<List<Litige>> GetAllAsync();
    }
}
