using RdC.Domain.Paiements;

namespace RdC.Application.Common.Interfaces
{
    public interface IPaiementRepository
    {
        Task<bool> AddAsync(Paiement paiement);
        Task<Paiement?> GetByIdAsync(int paiementId);
    }
}
