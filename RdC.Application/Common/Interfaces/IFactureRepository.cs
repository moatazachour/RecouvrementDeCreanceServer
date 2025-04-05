using RdC.Domain.DTO.Facture;
using RdC.Domain.Factures;

namespace RdC.Application.Common.Interfaces
{
    public interface IFactureRepository
    {
        Task<Facture?> GetByIdAsync(int factureID);
        Task<List<Facture>> ListAsync();
        Task<List<Facture>> GetByIdsAsync(List<int> factureIDs);
        Task<bool> AddFacturesAsync();
        Task<Facture?> UpdateAsync(int FactureID, FactureUpdate factureUpdate);
    }
}
