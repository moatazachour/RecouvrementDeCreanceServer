using RdC.Application.Common.Interfaces;
using RdC.Domain.Litiges;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Litiges.Persistance
{
    public class LitigeJustificatifRepository : ILitigeJustificatifRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public LitigeJustificatifRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LitigeJustificatif?> GetByIdAsync(int id)
        {
            return await _dbContext.Justificatifs.FindAsync(id);
        }
    }
}
