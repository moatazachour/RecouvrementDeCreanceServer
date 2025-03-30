namespace RdC.Domain.DTO.Paiement
{
    public record PaiementResponse(
        int PaiementID,
        int PlanID,
        decimal MontantPayee,
        DateTime DateDePaiement);
}
