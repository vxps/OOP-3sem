using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Algorithms;

public class SplitStorage : IStorageAlgorithm
{
    public IReadOnlyList<Storage> MakeReserveCopy(RestorePoint restorePoint, Repository repository, int counter)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        ArgumentNullException.ThrowIfNull(repository);

        var storages = restorePoint.BackupObjects.Select(x => new Storage(repository.DirPath + x.Name, x)).ToList();
        return storages;
    }
}