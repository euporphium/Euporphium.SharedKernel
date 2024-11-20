namespace Euporphium.SharedKernel;

public interface IEntity
{
    object Id { get; }
    bool IsTransient();
}

public interface IEntity<out TId> : IEntity where TId : IEquatable<TId>
{
    new TId Id { get; }
}