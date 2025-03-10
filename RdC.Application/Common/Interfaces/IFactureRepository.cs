using RdC.Domain.Factures;

namespace RdC.Application.Common.Interfaces
{
    public interface IFactureRepository
    {
        Task<Facture?> GetByIdAsync(int FactureID);
        Task<List<Facture>> ListAsync();
    }
}
