using RdC.Domain.Factures;
using System.Text.Json.Serialization;

namespace RdC.Domain.DTO.Facture
{
    public record FactureDtoForExternalAPI(
        int FactureID, 
        string NumFacture, 
        DateOnly DateDeEcheance, 
        decimal MontantTotal, 
        decimal MontantRestantDue, 
        int AcheteurID); 
}
