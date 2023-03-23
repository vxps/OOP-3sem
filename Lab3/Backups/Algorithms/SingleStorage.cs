using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Algorithms;

public class SingleStorage : IStorageAlgorithm
{
    public IReadOnlyList<Storage> MakeReserveCopy(RestorePoint restorePoint, Repository repository, int counter)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        ArgumentNullException.ThrowIfNull(repository);

        var storages = new List<Storage>();
        var storage = new Storage(repository.DirPath);
        foreach (BackupObject backupObject in restorePoint.BackupObjects)
        {
            storage.AddBackupObject(backupObject);
        }

        storages.Add(storage);

        return storages;
    }
}