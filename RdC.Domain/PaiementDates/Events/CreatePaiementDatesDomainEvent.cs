using RdC.Domain.Abstrations;

namespace RdC.Domain.PaiementDates.Events
{
    public sealed record CreatePaiementDatesDomainEvent(int PlanID) : IDomainEvent;
}
