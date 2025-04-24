using RdC.Domain.Litiges;

namespace RdC.Application.Common.Interfaces
{
    public interface ILitigeJustificatifRepository
    {
        Task<LitigeJustificatif?> GetByIdAsync(int id);
    }
}
