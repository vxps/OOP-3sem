using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Algorithms.Clean;

public class Merge : IMerger
{
    private const int MinObjectsCount = 0;
    public RestorePoint MergePoints(RestorePoint olderRestorePoint, RestorePoint youngerRestorePoint, BackupTaskExtra backupTaskExtra)
    {
        ArgumentNullException.ThrowIfNull(olderRestorePoint);
        ArgumentNullException.ThrowIfNull(youngerRestorePoint);
        ArgumentNullException.ThrowIfNull(backupTaskExtra);

        if (backupTaskExtra.StorageAlgorithm.GetType() == typeof(SingleStorage))
        {
            backupTaskExtra.DeleteRestorePoint(olderRestorePoint);

            return youngerRestorePoint;
        }

        if (olderRestorePoint.BackupObjects.Count != MinObjectsCount &&
            youngerRestorePoint.BackupObjects.Count != MinObjectsCount)
        {
            backupTaskExtra.DeleteRestorePoint(olderRestorePoint);

            return youngerRestorePoint;
        }

        if (olderRestorePoint.BackupObjects.Count != MinObjectsCount &&
            youngerRestorePoint.BackupObjects.Count == MinObjectsCount)
        {
            youngerRestorePoint = olderRestorePoint;
            backupTaskExtra.DeleteRestorePoint(olderRestorePoint);

            return youngerRestorePoint;
        }

        throw new ArgumentException("can't merge points");
    }
}