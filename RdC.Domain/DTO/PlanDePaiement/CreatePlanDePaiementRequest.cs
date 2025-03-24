namespace RdC.Domain.DTO.PlanDePaiement
{
    public record CreatePlanDePaiementRequest(
        decimal MontantTotal, 
        byte NombreDeEcheances, 
        decimal MontantDeChaqueEcheance,
        List<int> FactureIDs);
}
