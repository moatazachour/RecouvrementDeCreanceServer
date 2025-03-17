using RdC.Domain.Factures;

namespace RdC.Domain.DTO.Facture
{
    public record FactureResponse(int FactureID, string NumFacture, DateOnly DateEcheance, decimal MontantTotal,
            decimal MontantRestantDue, int AcheteurID, FactureStatus Status);
}
