using System.IO;
using System.IO.Compression;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Entities;

public class Repository
{
    private int _restorePointCounter = 1;
    public Repository(string dirPath)
    {
        if (string.IsNullOrWhiteSpace(dirPath))
            throw RepositoryExcepton.InvalidPathException();

        DirPath = dirPath;
    }

    public string DirPath { get; }
    public void SaveStorages(RestorePoint restorePoint, IStorageAlgorithm algo)
    {
        ArgumentNullException.ThrowIfNull(restorePoint);
        ArgumentNullException.ThrowIfNull(algo);

        IReadOnlyList<Storage> storages = algo.MakeReserveCopy(restorePoint, this, _restorePointCounter);

        foreach (Storage storage in storages)
        {
            File.Create(storage.Path + $"({_restorePointCounter}).zip");
        }

        _restorePointCounter++;
    }
}