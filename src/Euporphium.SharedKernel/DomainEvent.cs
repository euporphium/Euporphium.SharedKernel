using MediatR;

namespace Euporphium.SharedKernel;

public abstract class DomainEvent : INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}