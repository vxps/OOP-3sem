using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.Entities;

public class StudentsGroup : Group
{
    public StudentsGroup(GroupName name)
        : base(name)
    {
        StudentSchedule = new Schedule();
    }

    public Schedule StudentSchedule { get; private set; }

    public void AddSchedule(Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(schedule);

        StudentSchedule = schedule;
    }
}