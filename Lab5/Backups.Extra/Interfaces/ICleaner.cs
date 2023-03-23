using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface ICleaner
{
    void Clean(List<RestorePoint> restorePoints);
}