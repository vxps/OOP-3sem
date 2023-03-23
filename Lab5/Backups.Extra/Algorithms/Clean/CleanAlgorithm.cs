using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Algorithms.Clean;

public class CleanAlgorithm : ICleaner
{
    private BackupTaskExtra _backupTaskExtra;

    public CleanAlgorithm(BackupTaskExtra backupTaskExtra)
    {
        ArgumentNullException.ThrowIfNull(backupTaskExtra);

        _backupTaskExtra = backupTaskExtra;
    }

    public void Clean(List<RestorePoint> restorePoints)
    {
        ArgumentNullException.ThrowIfNull(restorePoints);

        foreach (RestorePoint restorePoint in restorePoints)
        {
            _backupTaskExtra.DeleteRestorePoint(restorePoint);
        }
    }
}