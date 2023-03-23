namespace Backups.Tools;

public class RepositoryExcepton : Exception
{
    public RepositoryExcepton(string message)
        : base(message) { }

    public static RepositoryExcepton InvalidPathException()
    {
        throw new RepositoryExcepton("name is null or empty");
    }

    public static RepositoryExcepton InvalidDirectoryException()
    {
        throw new RepositoryExcepton("no such directory");
    }
}