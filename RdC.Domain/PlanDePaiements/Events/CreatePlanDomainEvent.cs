using RdC.Domain.Abstrations;

namespace RdC.Domain.PlanDePaiements.Events
{
    public sealed record CreatePlanDomainEvent(
        int PlanID,
        decimal MontantDeChaqueEcheance) : IDomainEvent;
}
