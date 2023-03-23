using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class OgnpFlow
{
    public const int MaxStudentsInFlow = 10;
    private readonly List<ItmoStudent> _studentsInFlow;
    private readonly int _flowsIdCounter = 0;

    public OgnpFlow(string name, Schedule schedule, Megafaculty faculty)
    {
        ArgumentNullException.ThrowIfNull(faculty);
        if (string.IsNullOrWhiteSpace(name))
            throw OgnpFlowException.InvalidOgnpFlowNameException();

        Name = name;
        _studentsInFlow = new List<ItmoStudent>();
        IdCounter = _flowsIdCounter++;
        FlowSchedule = schedule;
        Faculty = faculty;
    }

    public IReadOnlyList<ItmoStudent> StudentsInFlow => _studentsInFlow;
    public string Name { get; }
    public int IdCounter { get; }
    public Megafaculty Faculty { get; }
    public Schedule FlowSchedule { get; }

    public bool CheckIsFull()
        => _studentsInFlow.Count >= MaxStudentsInFlow;

    public bool CheckStudentExist(ItmoStudent student)
        => _studentsInFlow.Contains(student);

    public ItmoStudent AddStudent(ItmoStudent student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (CheckIsFull())
            throw OgnpFlowException.InvalidFlowCountException();

        _studentsInFlow.Add(student);
        return student;
    }

    public ItmoStudent RemoveStudent(ItmoStudent student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (!CheckStudentExist(student))
            throw new OgnpFlowException("no such student in group");

        _studentsInFlow.Remove(student);
        return student;
    }
}