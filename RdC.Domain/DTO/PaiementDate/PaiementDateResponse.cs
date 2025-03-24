namespace RdC.Domain.DTO.PaiementDate
{
    public record PaiementDateResponse(
        int DateID,
        int PlanID,
        DateOnly EcheanceDate,
        bool IsPaid);
}
