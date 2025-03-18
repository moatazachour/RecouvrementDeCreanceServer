using RdC.Domain.DTO.Facture;
using RdC.Domain.Factures;

namespace RdC.Application.Common.Interfaces
{
    public interface IFactureRepository
    {
        Task<Facture?> GetByIdAsync(int FactureID);
        Task<List<Facture>> ListAsync();
        Task<bool> AddFacturesAsync();
        Task<Facture?> UpdateAsync(int FactureID, FactureUpdate factureUpdate);
    }
}
