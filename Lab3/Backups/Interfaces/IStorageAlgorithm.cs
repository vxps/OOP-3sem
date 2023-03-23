using Backups.Entities;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    IReadOnlyList<Storage> MakeReserveCopy(RestorePoint restorePoint, Repository repository, int counter);
}