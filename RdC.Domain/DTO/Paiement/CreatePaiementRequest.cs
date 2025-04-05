namespace RdC.Domain.DTO.Paiement
{
    public record CreatePaiementRequest(
        int PlanID,
        int PaiementDateID,
        decimal MontantPayee,
        DateTime DateDePaiement);
}
