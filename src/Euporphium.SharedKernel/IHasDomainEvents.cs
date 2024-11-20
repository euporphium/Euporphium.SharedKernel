namespace Euporphium.SharedKernel;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    IEnumerable<DomainEvent> ConsumeDomainEvents();
}