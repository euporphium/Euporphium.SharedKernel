namespace Euporphium.SharedKernel.Tests;

public class DomainEventTests
{
    [Fact]
    public void OccurredOn_ShouldBeSetToCurrentDateTime()
    {
        var before = DateTime.UtcNow;
        var domainEvent = new TestDomainEvent();

        domainEvent.OccurredOn.Should().BeOnOrAfter(before);
        domainEvent.OccurredOn.Should().BeOnOrBefore(DateTime.UtcNow);
        domainEvent.OccurredOn.Kind.Should().Be(DateTimeKind.Utc);
    }

    private class TestDomainEvent : DomainEvent
    {
    }
}