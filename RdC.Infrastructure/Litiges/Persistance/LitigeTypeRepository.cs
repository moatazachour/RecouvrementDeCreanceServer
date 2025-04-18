using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Litiges;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Litiges.Persistance
{
    public class LitigeTypeRepository : ILitigeTypeRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public LitigeTypeRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(LitigeType litigeType)
        {
            await _dbContext.LitigeTypes.AddAsync(litigeType);

            return true;
        }

        public Task<List<LitigeType>> GetAllAsync()
        {
            return _dbContext.LitigeTypes.ToListAsync();
        }

        public async Task<LitigeType?> GetByIdAsync(int litigeTypeID)
        {
            return await _dbContext.LitigeTypes.FindAsync(litigeTypeID);
        }

    }
}
