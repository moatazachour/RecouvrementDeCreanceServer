namespace RdC.Domain.DTO.PaiementDate
{
    public record PaiementDateSimpleResponse(
        int PlanID,
        DateOnly EcheanceDate,
        decimal MontantDeEcheance,
        decimal MontantPayee,
        decimal MontantDue,
        bool IsPaid,
        bool IsLocked);
}
