using Isu.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class ItmoStudent : Student
{
    private const int MaxOgnpCount = 2;
    private List<OgnpFlow> _studentFlows;
    public ItmoStudent(string name, StudentsGroup other, int id)
        : base(name, other, id)
    {
        ArgumentNullException.ThrowIfNull(other);

        StudentOgnp = new Ognp();
        GroupStudent = other;
        _studentFlows = new List<OgnpFlow>();
    }

    public Ognp? StudentOgnp { get; private set; }
    public StudentsGroup GroupStudent { get; }
    public IReadOnlyList<OgnpFlow> Flows => _studentFlows;

    public OgnpFlow SignUpToOgnpFlow(Ognp ognp, OgnpFlow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);
        ArgumentNullException.ThrowIfNull(ognp);
        if (_studentFlows.Count == MaxOgnpCount)
            throw ItmoStudentException.InvalidOgnpException("student's ognp is full");
        if (_studentFlows.Any(x => x.Faculty.MegafacultyName == flow.Faculty.MegafacultyName))
            throw ItmoStudentException.InvalidOgnpException("student can't sign up to ognp in his faculty");
        if (!GroupStudent.StudentSchedule.CheckIntersect(flow.FlowSchedule.Lessons[0]))
            throw ItmoStudentException.InvalidOgnpException("schedule intersects with that ognp");

        _studentFlows.Add(flow);
        StudentOgnp = ognp;
        return flow;
    }

    public OgnpFlow DeleteFlow(OgnpFlow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);
        if (!_studentFlows.Contains(flow))
            throw ItmoStudentException.InvalidOgnpException("no such ognp");

        _studentFlows.Remove(flow);
        return flow;
    }

    public Ognp DeleteOgnp(Ognp ognp)
    {
        ArgumentNullException.ThrowIfNull(ognp);

        StudentOgnp = null;
        return ognp;
    }
}