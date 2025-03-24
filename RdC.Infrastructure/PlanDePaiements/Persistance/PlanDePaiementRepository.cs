using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PlanDePaiements;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.PlanDePaiements.Persistance
{
    public class PlanDePaiementRepository : IPlanDePaiementRepository
    {
        private readonly RecouvrementDBContext _dbContext;

        public PlanDePaiementRepository(RecouvrementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(PlanDePaiement planDePaiement)
        {
            if (planDePaiement == null)
            {
                throw new ArgumentNullException(nameof(planDePaiement));
            }

            await _dbContext.PlanDePaiements.AddAsync(planDePaiement);

            return true;
        }

        public async Task<PlanDePaiement?> GetByIdAsync(int PlanID)
        {
            return await _dbContext.PlanDePaiements
                .Include(pp => pp.Factures)
                .Include(pp => pp.PaiementsDates)
                .FirstOrDefaultAsync(pp => pp.Id == PlanID);
        }
    }
}
