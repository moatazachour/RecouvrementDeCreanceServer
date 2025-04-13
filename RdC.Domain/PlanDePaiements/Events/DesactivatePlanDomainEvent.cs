using RdC.Domain.Abstrations;

namespace RdC.Domain.PlanDePaiements.Events
{
    public sealed record DesactivatePlanDomainEvent(
        int PlanID,
        int missedPaiementsCount)
        : IDomainEvent;
}
