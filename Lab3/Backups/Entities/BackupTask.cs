using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Entities;

public class BackupTask
{
    private List<BackupObject> _backupObjects;
    private List<RestorePoint> _restorePoints;
    public BackupTask(string name, IStorageAlgorithm algorithm, Repository repository)
    {
        ArgumentNullException.ThrowIfNull(algorithm);

        Name = name;
        Rep = repository;
        StorageAlgorithm = algorithm;
        _backupObjects = new List<BackupObject>();
        _restorePoints = new List<RestorePoint>();
    }

    public string Name { get; }
    public Repository Rep { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IReadOnlyList<BackupObject> Objects => _backupObjects;
    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public RestorePoint AddRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        _restorePoints.Add(restorePoint);
        restorePoint.AddListBackupObjects(this._backupObjects);

        return restorePoint;
    }

    public RestorePoint DeleteRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        _restorePoints.Remove(restorePoint);
        return restorePoint;
    }

    public RestorePoint SaveRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        Rep.SaveStorages(restorePoint, this.StorageAlgorithm);
        return restorePoint;
    }

    public BackupObject AddBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        _backupObjects.Add(backupObject);
        return backupObject;
    }

    public List<BackupObject> AddListBackupObjects(List<BackupObject> backupObjects)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);

        _backupObjects.AddRange(backupObjects);
        return backupObjects;
    }

    public BackupObject DeleteBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        _backupObjects.Remove(backupObject);
        return backupObject;
    }

    public void RunBackupTask(string nameOfRestorePoint)
    {
        var restorePoint = new RestorePoint(nameOfRestorePoint, DateTime.Now);
        restorePoint = AddRestorePoint(restorePoint);
        restorePoint = SaveRestorePoint(restorePoint);
    }
}