using RdC.Domain.PlanDePaiements;

namespace RdC.Application.Common.Interfaces
{
    public interface IPlanDePaiementRepository
    {
        Task<bool> AddAsync(PlanDePaiement planDePaiement);

        Task<PlanDePaiement?> GetByIdAsync(int PlanID);
    }
}
