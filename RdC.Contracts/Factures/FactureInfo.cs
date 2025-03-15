namespace RdC.Contracts.Factures
{
    public record FactureInfo(int FactureID, string NumFacture, DateOnly DateEcheance, decimal MontantTotal,
            decimal MontantRestantDue);
}
