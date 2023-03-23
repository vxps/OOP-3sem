namespace Backups.Tools;

public class BackupTaskException : Exception
{
    public BackupTaskException(string message)
        : base(message) { }

    public static BackupTaskException InvalidNameException()
    {
        throw new BackupTaskException("name is null or empty");
    }
}