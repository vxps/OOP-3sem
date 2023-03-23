using Backups.Entities;
using Backups.Extra.Entities;

namespace Backups.Extra.Interfaces;

public interface IMerger
{
    RestorePoint MergePoints(RestorePoint olderRestorePoint, RestorePoint youngerRestorePoint, BackupTaskExtra backupTaskExtra);
}