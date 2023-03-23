using Backups.Tools;

namespace Backups.Entities;

public class Storage
{
    private List<BackupObject> _backupObjects = new ();
    public Storage(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw StorageException.InvalidPathException();

        Path = path;
        _backupObjects = new List<BackupObject>();
    }

    public Storage(string path, List<BackupObject> objects)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw StorageException.InvalidPathException();
        ArgumentNullException.ThrowIfNull(objects);

        Path = path;
        _backupObjects.AddRange(objects);
    }

    public Storage(string path, BackupObject backupObject)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw StorageException.InvalidPathException();
        ArgumentNullException.ThrowIfNull(backupObject);

        Path = path;
        _backupObjects.Add(backupObject);
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public string Path { get; }

    public BackupObject AddBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        _backupObjects.Add(backupObject);
        return backupObject;
    }
}