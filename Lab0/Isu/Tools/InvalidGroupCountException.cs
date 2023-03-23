namespace Isu.Tools;

public class InvalidGroupCountException : IsuException
{
    public InvalidGroupCountException()
    {
    }

    public InvalidGroupCountException(string message)
        : base(message)
    {
    }

    public InvalidGroupCountException(string message, Exception inner)
        : base(message, inner)
    {
    }
}