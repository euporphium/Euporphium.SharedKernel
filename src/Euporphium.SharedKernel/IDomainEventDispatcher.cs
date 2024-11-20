namespace Euporphium.SharedKernel;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<IHasDomainEvents> entitiesWithEvents);
}