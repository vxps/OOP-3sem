using Backups.Extra.Entities;

namespace Backups.Extra.Interfaces;

public interface ISaver
{
    void SaveState(BackupTaskExtra backupTaskExtra, string path);
    BackupTaskExtra Deserialize(string path);
}