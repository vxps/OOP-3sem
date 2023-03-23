namespace Backups.Tools;

public class StorageException : Exception
{
    public StorageException(string message)
        : base(message) { }

    public static StorageException InvalidPathException()
    {
        throw new StorageException("name is null or empty");
    }
}