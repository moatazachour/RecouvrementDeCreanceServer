using RdC.Domain.Abstrations;

namespace RdC.Domain.Relances.Events
{
    public sealed record SendEmailDomainEvent(string email, string emailBody) : IDomainEvent;
}
