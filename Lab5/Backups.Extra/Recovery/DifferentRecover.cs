using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Recovery;

public class DifferentRecover
{
    public void Restore(RestorePoint restorePoint, Repository repository, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(restorePoint);
        ArgumentNullException.ThrowIfNull(logger);

        string path = repository.DirPath;
        foreach (BackupObject backup in restorePoint.BackupObjects)
        {
            File.Create(Path.Combine(path, backup.Name, ".zip"));
            logger.Log($"{backup} was restored from {restorePoint}", true);
        }

        logger.Log($"all data from {restorePoint} was restored", false);
    }
}