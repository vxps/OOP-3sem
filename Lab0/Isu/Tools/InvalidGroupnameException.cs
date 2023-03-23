namespace Isu.Tools;

public class InvalidGroupnameException : IsuException
{
    public InvalidGroupnameException()
    {
    }

    public InvalidGroupnameException(string message)
        : base(message)
    {
    }

    public InvalidGroupnameException(string message, Exception inner)
        : base(message, inner)
    {
    }
}