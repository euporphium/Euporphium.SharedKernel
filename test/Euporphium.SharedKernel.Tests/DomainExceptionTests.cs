namespace Euporphium.SharedKernel.Tests;

public class DomainExceptionTests
{
    [Fact]
    public void DefaultConstructor_ShouldCreateException()
    {
        var exception = new DomainException();

        exception.Should().NotBeNull();
        exception.Message.Should().NotBeNull();
        exception.InnerException.Should().BeNull();
    }

    [Fact]
    public void MessageConstructor_ShouldCreateExceptionWithMessage()
    {
        const string message = "Invalid domain operation";

        var exception = new DomainException(message);

        exception.Should().NotBeNull();
        exception.Message.Should().Be(message);
        exception.InnerException.Should().BeNull();
    }

    [Fact]
    public void MessageAndInnerExceptionConstructor_ShouldCreateExceptionWithBoth()
    {
        const string message = "Invalid domain operation";
        var innerException = new ArgumentException("Inner exception message");

        var exception = new DomainException(message, innerException);

        exception.Should().NotBeNull();
        exception.Message.Should().Be(message);
        exception.InnerException.Should().BeSameAs(innerException);
    }
}