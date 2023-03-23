namespace Isu.Tools;

public class InvalidStudentGetException : IsuException
{
    public InvalidStudentGetException()
    {
    }

    public InvalidStudentGetException(string message)
        : base(message)
    {
    }

    public InvalidStudentGetException(string message, Exception inner)
        : base(message, inner)
    {
    }
}