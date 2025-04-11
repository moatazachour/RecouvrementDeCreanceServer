namespace RdC.Domain.DTO.Paiement
{
    public record PaiementResponse(
        int PaiementID,
        int PaiementDateID,
        decimal MontantPayee,
        DateTime DateDePaiement);
}
