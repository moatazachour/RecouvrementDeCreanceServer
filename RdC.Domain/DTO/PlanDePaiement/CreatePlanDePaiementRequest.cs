using RdC.Domain.DTO.PaiementDate;

namespace RdC.Domain.DTO.PlanDePaiement
{
    public record CreatePlanDePaiementRequest(
        decimal MontantTotal, 
        byte NombreDeEcheances, 
        List<int> FactureIDs,
        bool HasAdvance,
        int CreatedByUserID);
}
