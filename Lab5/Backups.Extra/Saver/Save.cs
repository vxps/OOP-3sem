using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Newtonsoft.Json;

namespace Backups.Extra.Saver;

public class Save : ISaver
{
    public void SaveState(BackupTaskExtra backupTaskExtra, string path)
    {
        ArgumentNullException.ThrowIfNull(backupTaskExtra);
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("path is null or white space)");

        string json = JsonConvert.SerializeObject(backupTaskExtra);
        File.WriteAllText(path, json);
    }

    public BackupTaskExtra Deserialize(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("path is null or white space");

        return JsonConvert.DeserializeObject<BackupTaskExtra>(File.ReadAllText(path));
    }
}