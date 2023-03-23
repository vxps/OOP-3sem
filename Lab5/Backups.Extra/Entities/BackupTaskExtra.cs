using Backups.Entities;
using Backups.Extra.Algorithms.Limits;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : BackupTask
{
    public BackupTaskExtra(string name, IStorageAlgorithm algorithm, Repository repository, ILogger logger, bool timePrefix)
        : base(name, algorithm, repository)
    {
        ArgumentNullException.ThrowIfNull(logger);

        Logger = logger;
        TimePrefix = timePrefix;
    }

    public ILogger Logger { get; }
    public bool TimePrefix { get; }

    public RestorePoint AddExtraRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        AddRestorePoint(restorePoint);
        Logger.Log($"{restorePoint.Name} was added", TimePrefix);
        return restorePoint;
    }

    public RestorePoint DeleteExtraRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        DeleteRestorePoint(restorePoint);
        Logger.Log($"{restorePoint.Name} was deleted", TimePrefix);
        return restorePoint;
    }

    public BackupObject AddExtraBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        AddBackupObject(backupObject);
        Logger.Log($"{backupObject.Name} was added", TimePrefix);
        return backupObject;
    }

    public BackupObject DeleteExtraBackupObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        DeleteBackupObject(backupObject);
        Logger.Log($"{backupObject.Name} was deleted", TimePrefix);
        return backupObject;
    }

    public RestorePoint SaveExtraRestorePoint(RestorePoint restorePoint)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);

        SaveRestorePoint(restorePoint);
        Logger.Log($"{restorePoint.Name} saved", TimePrefix);
        return restorePoint;
    }

    public List<RestorePoint> ChoosePointsByLimit(ILimit limit)
    {
        ArgumentNullException.ThrowIfNull(limit);

        return limit.Choose(RestorePoints);
    }

    public void CleanRestorePoints(ICleaner cleaner, List<RestorePoint> restorePoints)
    {
        ArgumentNullException.ThrowIfNull(cleaner);
        ArgumentNullException.ThrowIfNull(restorePoints);

        cleaner.Clean(restorePoints);
    }

    public void MergePoints(RestorePoint oldRestorePoint, RestorePoint youngRestorePoint, IMerger merger)
    {
        ArgumentNullException.ThrowIfNull(oldRestorePoint);
        ArgumentNullException.ThrowIfNull(youngRestorePoint);
        ArgumentNullException.ThrowIfNull(merger);
        if (oldRestorePoint.Date < youngRestorePoint.Date)
            throw new ArgumentException("old points time is < than young points time");

        merger.MergePoints(oldRestorePoint, youngRestorePoint, this);
    }

    public void RunBackupExtra(string restorePointName, bool time)
    {
        if (string.IsNullOrWhiteSpace(restorePointName))
            throw new ArgumentException("restore point name can't be null or white space");
        ArgumentNullException.ThrowIfNull(time);

        RunBackupTask(restorePointName);
        Logger.Log($"{restorePointName} - restore point created", time);
    }
}