using RdC.Domain.Acheteurs;

namespace RdC.Application.Common.Interfaces
{
    public interface IAcheteurRepository
    {
        Task<Acheteur?> GetByIdAsync(int AcheteurID); 
        Task<List<Acheteur>> ListAsync();
        Task<bool> AddAcheteursAsync();
    }
}
