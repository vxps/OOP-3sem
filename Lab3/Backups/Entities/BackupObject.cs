using Backups.Tools;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw BackupObjectException.InvalidNameException();
        if (string.IsNullOrWhiteSpace(path))
            throw BackupObjectException.InvalidPathException();

        Name = name;
        Path = path;
    }

    public string Name { get; }
    public string Path { get; }
}