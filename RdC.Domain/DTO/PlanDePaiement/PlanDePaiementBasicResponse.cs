using RdC.Domain.PlanDePaiements;

namespace RdC.Domain.DTO.PlanDePaiement
{
    public record PlanDePaiementBasicResponse(
        int PlanID,
        decimal MontantTotal,
        byte nombreDeEcheances,
        decimal montantRestant,
        DateTime creationDate,
        PlanStatus planStatus,
        bool isLocked,
        bool hasAdvance);
}
