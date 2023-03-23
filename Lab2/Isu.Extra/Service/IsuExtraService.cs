using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;
using Isu.Models;
using Isu.Tools;

namespace Isu.Extra.Service;

public class IsuExtraService : IIsuExtraService
{
    private readonly List<OgnpFlow> _flows;
    private readonly List<ItmoStudent> _students;
    private readonly List<StudentsGroup> _allGroups;
    private readonly List<Ognp> _allOgnp;
    private int _isuNumberCounter = 100000;

    public IsuExtraService()
    {
        _flows = new List<OgnpFlow>();
        _students = new List<ItmoStudent>();
        _allGroups = new List<StudentsGroup>();
        _allOgnp = new List<Ognp>();
    }

    public bool CheckGroupExist(string name)
        => _allGroups.Any(other => other.GroupName.Name.Equals(name));

    public bool CheckOgnpExist(string name)
        => _allOgnp.Any(other => other.Name is not null && other.Name.Equals(name));

    public bool CheckFlowExist(string name)
        => _flows.Any(other => other.Name.Equals(name));
    public ItmoStudent AddStudent(string name, StudentsGroup group)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ItmoStudentException.InvalidOgnpException("wrong student name");
        ArgumentNullException.ThrowIfNull(group);

        var student = new ItmoStudent(name, group, _isuNumberCounter++);
        if (!CheckGroupExist(group.GroupName.Name))
            group = AddGroup(group.GroupName.Name);
        _students.Add(student);
        group.AddStudent(student);

        return student;
    }

    public StudentsGroup AddGroup(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidGroupnameException("wrong group name");
        if (CheckGroupExist(name))
            throw new InvalidGroupnameException("such group exist");

        var group = new StudentsGroup(new GroupName(name));
        _allGroups.Add(group);

        return group;
    }

    public Schedule AddGroupsSchedule(StudentsGroup group, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(schedule);
        if (!CheckGroupExist(group.GroupName.Name))
            throw new InvalidGroupnameException("such group exist");

        group.AddSchedule(schedule);

        return schedule;
    }

    public Ognp AddOgnp(string name, Megafaculty megafaculty)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw OgnpException.InvalidOgnpNameException();
        ArgumentNullException.ThrowIfNull(megafaculty);
        if (CheckOgnpExist(name))
            throw new OgnpException("such ognp exist");

        var ognp = new Ognp(name, megafaculty);
        _allOgnp.Add(ognp);

        return ognp;
    }

    public OgnpFlow AddOgnpFlow(string name, Schedule schedule, Megafaculty megafaculty, Ognp ognp)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw OgnpFlowException.InvalidOgnpFlowNameException();
        ArgumentNullException.ThrowIfNull(schedule);
        if (CheckFlowExist(name))
            throw new OgnpFlowException("such flow exist");

        var flow = new OgnpFlow(name, schedule, megafaculty);
        ognp.AddFlow(flow);
        _flows.Add(flow);
        return flow;
    }

    public ItmoStudent SignUpStudentToOgnpFlow(ItmoStudent student, Ognp ognp, OgnpFlow flow)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(flow);
        if (flow.StudentsInFlow.Contains(student))
            throw new OgnpFlowException("student has this flow");
        if (!ognp.CheckFlowExist(flow))
            throw new OgnpFlowException("such flow don't exist");

        student.SignUpToOgnpFlow(ognp, flow);
        flow.AddStudent(student);

        return student;
    }

    public ItmoStudent DeleteStudentFlow(ItmoStudent student, OgnpFlow flow)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(flow);
        if (!flow.StudentsInFlow.Contains(student))
            throw new OgnpFlowException("flow don't have this student");
        if (!CheckFlowExist(flow.Name))
            throw new OgnpFlowException("such flow don't exist");

        flow.RemoveStudent(student);
        student.DeleteFlow(flow);

        return student;
    }

    public ItmoStudent DeleteStudentOgnp(ItmoStudent student, Ognp ognp)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(ognp);

        student.DeleteOgnp(ognp);
        return student;
    }

    public IReadOnlyList<OgnpFlow> GetFlowsInOgnp(Ognp ognp)
    {
        ArgumentNullException.ThrowIfNull(ognp);
        if (!ognp.Flows.Any())
            throw new OgnpException("ognp don't have any flows");

        return ognp.Flows;
    }

    public IReadOnlyList<ItmoStudent> GetStudentsFromOgnp(Ognp ognp)
    {
        ArgumentNullException.ThrowIfNull(ognp);
        if (!ognp.Flows.Any())
            throw new OgnpFlowException("no flows in ognp");

        return ognp.Flows.SelectMany(x => x.StudentsInFlow).ToList();
    }

    public IReadOnlyList<ItmoStudent> GetStudentsWithoutOgnp()
    {
        return _students.Where(x => x.Flows.Count == 0).ToList();
    }
}