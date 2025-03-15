namespace RdC.Contracts.Factures
{
    public record FactureResponse(int FactureID, string NumFacture, DateOnly DateEcheance, decimal MontantTotal,
            decimal MontantRestantDue, int AcheteurID, FactureStatus Status);
}
