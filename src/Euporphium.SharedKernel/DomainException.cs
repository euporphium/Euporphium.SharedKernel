namespace Euporphium.SharedKernel;

/// <summary>
/// Represents errors that occur during application execution in the domain layer.
/// </summary>
public class DomainException : Exception
{
    public DomainException()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, Exception inner)
        : base(message, inner)
    {
    }
}