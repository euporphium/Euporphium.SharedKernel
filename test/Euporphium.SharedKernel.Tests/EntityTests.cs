namespace Euporphium.SharedKernel.Tests;

public class EntityTests
{
    [Fact]
    public void Constructor_ShouldSetId()
    {
        var entity = new TestEntity(42);

        entity.Id.Should().Be(42);
    }

    [Fact]
    public void DefaultConstructor_ShouldSetDefaultId()
    {
        var entity = new TestEntity();

        entity.Id.Should().Be(default);
    }

    [Fact]
    public void IsTransient_WithDefaultId_ShouldReturnTrue()
    {
        var entity = new TestEntity();

        entity.IsTransient().Should().BeTrue();
    }

    [Fact]
    public void IsTransient_WithNonDefaultId_ShouldReturnFalse()
    {
        var entity = new TestEntity(42);

        entity.IsTransient().Should().BeFalse();
    }

    [Fact]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        var entity = new TestEntity(42);

        entity.Equals(entity).Should().BeTrue();
        entity.Equals(entity as object).Should().BeTrue();
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        var entity = new TestEntity(42);

        entity.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Equals_WithDifferentType_ShouldReturnFalse()
    {
        var entity1 = new TestEntity(42);
        var entity2 = new DifferentTestEntity(42);

        entity1.Equals(entity2).Should().BeFalse();
    }

    [Fact]
    public void Equals_WithTransientEntities_ShouldReturnFalse()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        entity1.Equals(entity2).Should().BeFalse();
    }

    [Fact]
    public void Equals_WithSameIdAndType_ShouldReturnTrue()
    {
        var entity1 = new TestEntity(42);
        var entity2 = new TestEntity(42);

        entity1.Equals(entity2).Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentIds_ShouldReturnFalse()
    {
        var entity1 = new TestEntity(42);
        var entity2 = new TestEntity(43);

        entity1.Equals(entity2).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ForTransientEntity_ShouldUseBaseHashCode()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ForNonTransientEntity_ShouldUseIdHashCode()
    {
        var entity1 = new TestEntity(42);
        var entity2 = new TestEntity(42);

        entity1.GetHashCode().Should().Be(entity2.GetHashCode());
    }

    [Fact]
    public void EqualityOperator_WithEqualEntities_ShouldReturnTrue()
    {
        var entity1 = new TestEntity(42);
        var entity2 = new TestEntity(42);

        (entity1 == entity2).Should().BeTrue();
    }

    [Fact]
    public void InequalityOperator_WithDifferentEntities_ShouldReturnTrue()
    {
        var entity1 = new TestEntity(42);
        var entity2 = new TestEntity(43);

        (entity1 != entity2).Should().BeTrue();
    }

    [Fact]
    public void EqualityOperator_WithNullOperands_ShouldHandleCorrectly()
    {
        TestEntity? entity1 = null;
        TestEntity? entity2 = null;
        var entity3 = new TestEntity(42);

        // Using parentheses to make the operator comparisons more explicit
        (entity1 == entity2).Should().BeTrue("null == null should be true");
        (entity1 == entity3).Should().BeFalse("null == object should be false");
        (entity3 == entity1).Should().BeFalse("object == null should be false");
    }

    [Fact]
    public void ExplicitIEntityInterface_WithIntegerId_ShouldReturnSameIdAsTypedProperty()
    {
        var entity = new TestEntity(42);
        var iEntity = (IEntity)entity;

        var explicitId = iEntity.Id;

        explicitId.Should().Be(entity.Id);
    }

    [Fact]
    public void ExplicitIEntityInterface_WithGuidId_ShouldReturnSameIdAsTypedProperty()
    {
        var id = Guid.NewGuid();
        var entity = new TestEntityWithGuidId(id);
        var iEntity = (IEntity)entity;

        var explicitId = iEntity.Id;

        explicitId.Should().Be(entity.Id);
    }

    private class TestEntity : Entity<int>
    {
        public TestEntity(int id) : base(id)
        {
        }

        public TestEntity() { } // For testing default constructor
    }

    private class TestEntityWithGuidId : Entity<Guid>
    {
        public TestEntityWithGuidId(Guid id) : base(id)
        {
        }

        public TestEntityWithGuidId() { } // For testing default constructor
    }

    private class DifferentTestEntity(int id) : Entity<int>(id);
}