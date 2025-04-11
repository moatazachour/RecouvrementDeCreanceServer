using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.PaiementDates.Persistance
{
    public class PaiementDateRepository : IPaiementDateRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public PaiementDateRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(List<PaiementDate> paiementDateList)
        {
            await _dbContext.PaiementDates.AddRangeAsync(paiementDateList);

            return true;
        }

        public async Task<PaiementDate?> GetByIdAsync(int DateDeEcheeanceID)
        {
            return await _dbContext.PaiementDates
                .Include(pd => pd.PlanDePaiement)
                .Include(pd => pd.Paiements)
                .FirstOrDefaultAsync(pd => pd.Id == DateDeEcheeanceID);
        }

        public async Task<List<PaiementDate>> GetByPlanIdAsync(int PlanID)
        {
            return await _dbContext.PaiementDates.Where(pd => pd.PlanID == PlanID).ToListAsync();
        }
    }
}
