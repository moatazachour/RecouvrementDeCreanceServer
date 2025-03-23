using RdC.Application.Common.Interfaces;
using RdC.Domain.PlanDePaiements;

namespace RdC.Infrastructure.PlanDePaiements.Persistance
{
    public class PlanDePaiementRepository : IPlanDePaiement
    {
        public Task<bool> AddAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlanDePaiement> GetPlanDePaiementAsync()
        {
            throw new NotImplementedException();
        }
    }
}
