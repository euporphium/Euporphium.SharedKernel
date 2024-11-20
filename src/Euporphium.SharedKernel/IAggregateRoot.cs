namespace Euporphium.SharedKernel;

public interface IAggregateRoot : IEntity
{
}

public interface IAggregateRoot<out TId> : IAggregateRoot, IEntity<TId>
    where TId : IEquatable<TId>
{
}