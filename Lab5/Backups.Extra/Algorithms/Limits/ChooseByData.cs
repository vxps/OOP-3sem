using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Extra.Tools;

namespace Backups.Extra.Algorithms.Limits;

public class ChooseByData : ILimit
{
    private readonly DateTime _timeInterval;

    public ChooseByData(DateTime time)
    {
        ArgumentNullException.ThrowIfNull(time);

        _timeInterval = time;
    }

    public List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints)
    {
        ArgumentNullException.ThrowIfNull(restorePoints);

        var restore = restorePoints.Where(x => x.Date > _timeInterval).ToList();
        if (restore.Count == restorePoints.Count)
            throw AlgorithmsException.InvalidChooseException();

        return restore;
    }
}