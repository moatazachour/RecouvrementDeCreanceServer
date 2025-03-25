using RdC.Domain.Abstrations;

namespace RdC.Application.Common.Dispatcher
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync(Entity entity);
    }
}
