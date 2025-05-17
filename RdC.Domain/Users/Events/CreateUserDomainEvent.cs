using RdC.Domain.Abstrations;

namespace RdC.Domain.Users.Events
{
    public record CreateUserDomainEvent(int userID) : IDomainEvent;
}
