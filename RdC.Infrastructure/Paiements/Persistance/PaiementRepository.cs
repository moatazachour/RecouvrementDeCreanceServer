using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Paiements;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Paiements.Persistance
{
    public class PaiementRepository : IPaiementRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public PaiementRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Paiement?> GetByIdAsync(int paiementId)
        {
            return await _dbContext.Paiements
                .FirstOrDefaultAsync(p => p.Id == paiementId);
        }

        async Task<bool> IPaiementRepository.AddAsync(Paiement paiement)
        {
            await _dbContext.Paiements.AddAsync(paiement);

            return true;
        }
    }
}
