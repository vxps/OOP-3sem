using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Recovery;

public class OriginalRecover
{
    public void Restore(RestorePoint restorePoint, BackupObject backupObject, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        ArgumentNullException.ThrowIfNull(backupObject);
        ArgumentNullException.ThrowIfNull(logger);

        string path = backupObject.Path;
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        foreach (BackupObject backup in restorePoint.BackupObjects)
        {
            File.Create(Path.Combine(path, backup.Name, ".zip"));
            logger.Log($"{backup} was restored from {restorePoint}", true);
        }

        logger.Log($"all data from {restorePoint} was restored", false);
    }
}