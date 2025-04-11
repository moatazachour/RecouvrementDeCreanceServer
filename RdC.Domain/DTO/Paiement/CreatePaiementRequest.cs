namespace RdC.Domain.DTO.Paiement
{
    public record CreatePaiementRequest(
        int PaiementDateID,
        decimal MontantPayee,
        DateTime DateDePaiement);
}
