namespace Isu.Tools;

public class InvalidStudentNameException : IsuException
{
    public InvalidStudentNameException() { }

    public InvalidStudentNameException(string message)
        : base(message) { }

    public InvalidStudentNameException(string message, Exception inner)
        : base(message, inner) { }
}