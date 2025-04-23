using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Litiges;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Litiges.Persistance
{
    public class LitigeRepository : ILitigeRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public LitigeRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Litige litige)
        {
            await _dbContext.Litiges.AddAsync(litige);

            return true;
        }

        public Task<List<Litige>> GetAllAsync()
        {
            return _dbContext.Litiges
                .Include(l => l.Facture)
                .Include(l => l.LitigeType)
                .Include(l => l.Justificatifs)
                .ToListAsync();
        }

        public Task<Litige?> GetByIdAsync(int litigeID)
        {
            return _dbContext.Litiges
                .Include(l => l.Facture)
                .Include(l => l.LitigeType)
                .Include(l => l.Justificatifs)
                .FirstOrDefaultAsync(l => l.Id == litigeID);
        }
    }
}
