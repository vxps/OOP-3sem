namespace Backups.Tools;

public class BackupObjectException : Exception
{
    public BackupObjectException(string message)
        : base(message) { }

    public static BackupObjectException InvalidNameException()
    {
        throw new BackupObjectException("name is null or empty");
    }

    public static BackupObjectException InvalidPathException()
    {
        throw new BackupObjectException("path is null or empty");
    }
}