using RdC.Domain.Litiges;

namespace RdC.Application.Common.Interfaces
{
    public interface ILitigeTypeRepository
    {
        Task<bool> AddAsync(LitigeType litigeType);

        Task<LitigeType?> GetByIdAsync(int litigeTypeID);

        Task<List<LitigeType>> GetAllAsync();
    }
}
