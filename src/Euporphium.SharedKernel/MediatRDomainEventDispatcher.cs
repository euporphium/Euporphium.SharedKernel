using MediatR;

namespace Euporphium.SharedKernel;

public class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchEventsAsync(IEnumerable<IHasDomainEvents> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            foreach (var domainEvent in entity.ConsumeDomainEvents())
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}