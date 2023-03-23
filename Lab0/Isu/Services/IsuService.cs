using System.Collections.Generic;
using Isu.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _allGroups;
    private readonly List<Student> _allStudentsList;
    private int _isuNumbersCounter = 100000;

    public IsuService()
    {
        _allGroups = new List<Group>();
        _allStudentsList = new List<Student>();
    }

    public bool CheckGroupExist(GroupName name) => _allGroups.Any(group => group.GroupName.Equals(name));

    public bool StudentExistInGroup(Group group, string name) =>
        group.StudentsInGroup.Any(student => student.Name.Equals(name));
    public Group AddGroup(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        if (CheckGroupExist(name))
            throw new InvalidGroupnameException("such group exist");

        var group = new Group(name);
        _allGroups.Add(group);

        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (group.IsFull) throw new InvalidGroupCountException("Group is full");
        if (StudentExistInGroup(group, name))
            throw new InvalidStudentNameException("such student exist");

        var student = new Student(name, group, _isuNumbersCounter);
        group.AddStudent(student);
        _isuNumbersCounter++;
        _allStudentsList.Add(student);

        return student;
    }

    public Student GetStudent(int id)
    {
        Student? student = FindStudent(id);
        if (student is null) throw new InvalidStudentGetException("no such student");

        return student;
    }

    public Student? FindStudent(int id)
    {
        return _allStudentsList.FirstOrDefault(student => student.IsuNumber == id);
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return _allGroups.FirstOrDefault(group => group.GroupName == groupName)?.StudentsInGroup ?? new List<Student>();
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return _allGroups.SelectMany(group => group.StudentsInGroup)
            .Where(student => student.Group.CourseNumber.Equals(courseNumber)).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _allGroups.FirstOrDefault(group => group.GroupName == groupName) ?? null;
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        return _allGroups.Where(numberOfCourse => numberOfCourse.CourseNumber.Equals(courseNumber)).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.Group.RemoveStudent(student);
        student.TransferStudent(newGroup);
        newGroup.AddStudent(student);
    }
}