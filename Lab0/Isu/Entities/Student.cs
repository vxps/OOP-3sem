using Isu.Tools;

namespace Isu.Entities;

public class Student
{
    public Student(string name, Group group, int id)
    {
        ArgumentNullException.ThrowIfNull(group);
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidStudentNameException("wrong student name");

        Name = name;
        Group = group;
        IsuNumber = id;
    }

    public string Name { get; }
    public Group Group { get; private set; }
    public int IsuNumber { get; }
    public void TransferStudent(Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(newGroup);
        this.Group = newGroup;
    }
}