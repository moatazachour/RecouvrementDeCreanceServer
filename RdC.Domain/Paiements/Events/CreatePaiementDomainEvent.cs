using RdC.Domain.Abstrations;

namespace RdC.Domain.Paiements.Events
{
    public sealed record class CreatePaiementDomainEvent(int PaiementID, int AcheteurID) : IDomainEvent;
}
