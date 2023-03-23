namespace Isu.Extra.Tools;

public class ItmoStudentException : Exception
{
    public ItmoStudentException(string message)
        : base(message) { }

    public static ItmoStudentException InvalidOgnpException(string message)
    {
        throw new ItmoStudentException(message);
    }
}