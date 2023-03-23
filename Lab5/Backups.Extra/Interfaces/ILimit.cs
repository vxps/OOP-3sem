using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface ILimit
{
    List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints);
}