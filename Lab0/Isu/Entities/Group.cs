using System;
using System.Collections.Generic;
using Isu.Models;
using Isu.Tools;

namespace Isu.Entities;

public class Group
{
    private const int MaximumOfStudents = 10;
    private readonly List<Student> _studentsInGroup;
    public Group(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        GroupName = name;
        _studentsInGroup = new List<Student>();
        CourseNumber = new CourseNumber(int.Parse(name.Name.Substring(2, 1)));
    }

    public GroupName GroupName { get; }
    public IReadOnlyList<Student> StudentsInGroup => _studentsInGroup;
    public CourseNumber CourseNumber { get; }

    public int MaxOfStudents => MaximumOfStudents;

    public bool IsFull => _studentsInGroup.Count >= MaximumOfStudents;

    public Student AddStudent(Student student)
    {
        if (IsFull) throw new InvalidGroupCountException("group is full");
        ArgumentNullException.ThrowIfNull(student);

        _studentsInGroup.Add(student);
        return student;
    }

    public void RemoveStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        _studentsInGroup.Remove(student);
    }
}