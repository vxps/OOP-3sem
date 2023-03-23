namespace Isu.Tools;

public class InvalidCourseNumberException : IsuException
{
    public InvalidCourseNumberException()
    {
    }

    public InvalidCourseNumberException(string message)
        : base(message)
    {
    }

    public InvalidCourseNumberException(string message, Exception inner)
        : base(message, inner)
    {
    }
}