using RdC.Domain.Abstrations;

namespace RdC.Domain.Users.Events
{
    public record ContinueRegistrationDomainEvent(int userID) : IDomainEvent;
}
