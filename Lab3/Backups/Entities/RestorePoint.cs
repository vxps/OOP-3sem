using Backups.Tools;

namespace Backups.Entities;

public class RestorePoint
{
    private List<BackupObject> _backupObjects;
    private List<Storage> _storages;

    public RestorePoint(string name, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(date);
        if (string.IsNullOrWhiteSpace(name))
            throw RestorePointException.InvalidNameException();

        Name = name;
        Date = date;
        _backupObjects = new List<BackupObject>();
        _storages = new List<Storage>();
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;

    public IReadOnlyList<Storage> Storages => _storages;
    public string Name { get; }
    public DateTime Date { get; }

    public BackupObject AddBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        _backupObjects.Add(backupObject);
        return backupObject;
    }

    public List<BackupObject> AddListBackupObjects(List<BackupObject> objects)
    {
        ArgumentNullException.ThrowIfNull(objects);

        _backupObjects.AddRange(objects);
        return objects;
    }

    public BackupObject DeleteBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        _backupObjects.Remove(backupObject);
        return backupObject;
    }
}