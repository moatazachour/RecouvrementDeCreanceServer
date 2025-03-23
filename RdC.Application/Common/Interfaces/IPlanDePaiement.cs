using RdC.Domain.PlanDePaiements;

namespace RdC.Application.Common.Interfaces
{
    public interface IPlanDePaiement
    {
        Task<bool> AddAsync();

        Task<PlanDePaiement> GetPlanDePaiementAsync();
    }
}
