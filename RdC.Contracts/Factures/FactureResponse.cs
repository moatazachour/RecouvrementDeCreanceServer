namespace RdC.Contracts.Factures
{
    public record FactureResponse(int FactureID, string NumFacture, DateTime DateEcheance, decimal MontantTotal,
            decimal MontantRestantDue, int AcheteurID);
}
