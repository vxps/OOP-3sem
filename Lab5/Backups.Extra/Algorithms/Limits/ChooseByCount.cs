using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Extra.Tools;

namespace Backups.Extra.Algorithms.Limits;

public class ChooseByCount : ILimit
{
    private const int MinCountRestorePoints = 0;
    private int _countRestorePoints;

    public ChooseByCount(int countRestorePoints)
    {
        ArgumentNullException.ThrowIfNull(countRestorePoints);
        if (countRestorePoints < MinCountRestorePoints)
            throw AlgorithmsException.InvalidCountInAlgorithmsException();

        _countRestorePoints = countRestorePoints;
    }

    public List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints)
    {
        ArgumentNullException.ThrowIfNull(restorePoints);
        var restore = restorePoints.Take(restorePoints.Count - _countRestorePoints).ToList();

        return restore;
    }
}