using System.ComponentModel.DataAnnotations.Schema;

namespace Euporphium.SharedKernel;

public abstract class Entity<TId> : IHasDomainEvents, IEntity<TId> where TId : IEquatable<TId>
{
    private readonly List<DomainEvent> _domainEvents = [];
    
    protected Entity(TId id) { Id = id; }
    protected Entity() { } // ORM

    public TId Id { get; } = default!;
    object IEntity.Id => Id; // Explicit interface implementation

    [NotMapped] public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public bool IsTransient() => EqualityComparer<TId>.Default.Equals(Id, default);

    protected void RegisterDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IEnumerable<DomainEvent> ConsumeDomainEvents()
    {
        var domainEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return domainEvents;
    }

    #region Equality

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        if (GetType() != other.GetType())
            return false;
        if (IsTransient() || other.IsTransient())
            return false;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj) => Equals(obj as Entity<TId>);

    // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode - Justification: Id.GetHashCode() is used for non-transient entities
    public override int GetHashCode() => IsTransient() ? base.GetHashCode() : Id.GetHashCode();

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => left?.Equals(right) ?? right is null;

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

    #endregion Equality
}