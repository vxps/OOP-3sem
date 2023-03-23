using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Extra.Tools;

namespace Backups.Extra.Algorithms.Limits;

public class HybridChoose : ILimit
{
    private const int MinRestorePoints = 0;
    private DateTime _date;
    private int _countRestorePoints;
    private bool _isBothNeed;
    public HybridChoose(int countRestorePoints, DateTime date, bool isBothNeed = true)
    {
        ArgumentNullException.ThrowIfNull(date);
        if (countRestorePoints < MinRestorePoints)
            throw AlgorithmsException.InvalidCountInAlgorithmsException();
        ArgumentNullException.ThrowIfNull(isBothNeed);

        _date = date;
        _countRestorePoints = countRestorePoints;
        _isBothNeed = isBothNeed;
    }

    public List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints)
    {
        ArgumentNullException.ThrowIfNull(restorePoints);

        List<RestorePoint> restore;
        if (_isBothNeed)
        {
            restore = restorePoints.Take(restorePoints.Count - _countRestorePoints).Where(x => x.Date < _date).ToList();
        }
        else
        {
            restore = restorePoints.Take(restorePoints.Count - _countRestorePoints).ToList();
            var newRestorePoints = restorePoints.TakeLast(_countRestorePoints).ToList();
            if (restorePoints.Count - _countRestorePoints > MinRestorePoints)
                restore.AddRange(newRestorePoints.Where(x => x.Date > _date));
        }

        return restore;
    }
}