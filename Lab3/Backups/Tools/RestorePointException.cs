namespace Backups.Tools;

public class RestorePointException : Exception
{
    public RestorePointException(string message)
    : base(message) { }

    public static RestorePointException InvalidNameException()
    {
        throw new RestorePointException("name is null or empty");
    }
}
