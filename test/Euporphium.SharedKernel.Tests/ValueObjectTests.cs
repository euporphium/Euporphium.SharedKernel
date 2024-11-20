namespace Euporphium.SharedKernel.Tests;

public class ValueObjectTests
{
    [Fact]
    public void EqualValueObjects_ShouldBeEqual()
    {
        var obj1 = new TestValueObject(1, "test");
        var obj2 = new TestValueObject(1, "test");

        obj1.Should().Be(obj2);
        obj1.Equals(obj2).Should().BeTrue();
        (obj1 == obj2).Should().BeTrue();
        (obj1 != obj2).Should().BeFalse();
    }

    [Fact]
    public void DifferentValueObjects_ShouldNotBeEqual()
    {
        var obj1 = new TestValueObject(1, "test");
        var obj2 = new TestValueObject(2, "test");

        obj1.Should().NotBe(obj2);
        obj1.Equals(obj2).Should().BeFalse();
        (obj1 == obj2).Should().BeFalse();
        (obj1 != obj2).Should().BeTrue();
    }

    [Fact]
    public void SameValues_ShouldHaveSameHashCode()
    {
        var obj1 = new TestValueObject(1, "test");
        var obj2 = new TestValueObject(1, "test");

        obj1.GetHashCode().Should().Be(obj2.GetHashCode());
    }

    [Fact]
    public void DifferentValues_ShouldHaveDifferentHashCode()
    {
        var obj1 = new TestValueObject(1, "test");
        var obj2 = new TestValueObject(2, "test");

        obj1.GetHashCode().Should().NotBe(obj2.GetHashCode());
    }

    [Fact]
    public void NullComparison_ShouldReturnFalse()
    {
        var obj = new TestValueObject(1, "test");

        obj.Equals(null).Should().BeFalse();
        (obj == null).Should().BeFalse();
        (null == obj).Should().BeFalse();
        (obj != null).Should().BeTrue();
        (null != obj).Should().BeTrue();
    }

    [Fact]
    public void DifferentTypes_ShouldNotBeEqual()
    {
        var obj1 = new TestValueObject(1, "test");
        var obj2 = new DifferentTestValueObject(1);

        obj1.Equals(obj2).Should().BeFalse();
    }

    [Fact]
    public void NullNullComparison_ShouldBeTrue()
    {
        TestValueObject? obj1 = null;
        TestValueObject? obj2 = null;

        (obj1 == obj2).Should().BeTrue("null should equal null");
        (obj1 != obj2).Should().BeFalse("null should equal null");
    }

    private class TestValueObject(int value1, string value2) : ValueObject
    {
        public int Value1 { get; } = value1;
        public string Value2 { get; } = value2;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value1;
            yield return Value2;
        }
    }

    private class DifferentTestValueObject(int value) : ValueObject
    {
        public int Value { get; } = value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}