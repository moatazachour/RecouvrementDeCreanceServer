using RdC.Domain.Abstrations;

namespace RdC.Domain.Litiges.Events
{
    public sealed record DeclareLitigeDomainEvent(int litigeID) : IDomainEvent;
}
