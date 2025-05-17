namespace RdC.Domain.DTO.PlanDePaiement
{
    public record ActivatePlanDePaiementRequest(
        int planID,
        int activatedByUserID);
}
