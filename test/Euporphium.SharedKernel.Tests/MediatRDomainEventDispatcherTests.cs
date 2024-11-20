using MediatR;
using Moq;

namespace Euporphium.SharedKernel.Tests;

public class MediatRDomainEventDispatcherTests
{
    [Fact]
    public async Task DispatchEventsAsync_WhereEntitiesHaveIntId_ShouldPublishAndClear()
    {
        var mediatorMock = new Mock<IMediator>();
        var domainEventDispatcher = new MediatRDomainEventDispatcher(mediatorMock.Object);
        var entity = new TestEntity();
        entity.AddTestDomainEvent();

        await domainEventDispatcher.DispatchEventsAsync(new List<Entity<int>> { entity });
        mediatorMock.Verify(m => m.Publish(It.IsAny<DomainEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public async Task DispatchEventsAsync_WhereEntitiesHaveIntGuid_ShouldPublishAndClear()
    {
        var mediatorMock = new Mock<IMediator>();
        var domainEventDispatcher = new MediatRDomainEventDispatcher(mediatorMock.Object);
        var entity = new TestEntityGuid();
        entity.AddTestDomainEvent();

        await domainEventDispatcher.DispatchEventsAsync(new List<Entity<Guid>> { entity });
        mediatorMock.Verify(m => m.Publish(It.IsAny<DomainEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public async Task DispatchEventsAsync_WhereEntitiesHaveMixedIds_ShouldPublishAndClear()
    {
        var mediatorMock = new Mock<IMediator>();
        var domainEventDispatcher = new MediatRDomainEventDispatcher(mediatorMock.Object);
        var entity = new TestEntity();
        var entityGuid = new TestEntityGuid();
        entity.AddTestDomainEvent();
        entityGuid.AddTestDomainEvent();

        await domainEventDispatcher.DispatchEventsAsync(new List<IHasDomainEvents> { entity, entityGuid });

        mediatorMock.Verify(m => m.Publish(It.IsAny<DomainEvent>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        entity.DomainEvents.Should().BeEmpty();
        entityGuid.DomainEvents.Should().BeEmpty();
    }

    private class TestDomainEvent : DomainEvent
    {
    }

    private class TestEntity : Entity<int>
    {
        public void AddTestDomainEvent()
        {
            var domainEvent = new TestDomainEvent();
            RegisterDomainEvent(domainEvent);
        }
    }

    private class TestEntityGuid : Entity<Guid>
    {
        public void AddTestDomainEvent()
        {
            var domainEvent = new TestDomainEvent();
            RegisterDomainEvent(domainEvent);
        }
    }
}