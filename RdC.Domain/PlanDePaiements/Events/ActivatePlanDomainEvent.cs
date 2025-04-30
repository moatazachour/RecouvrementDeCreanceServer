using RdC.Domain.Abstrations;

namespace RdC.Domain.PlanDePaiements.Events
{
    public sealed record ActivatePlanDomainEvent(
        PlanDePaiement PlanDePaiement) 
        : IDomainEvent;
}
