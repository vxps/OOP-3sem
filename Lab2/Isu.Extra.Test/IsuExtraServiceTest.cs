using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Service;
using Isu.Extra.Tools;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    private IIsuExtraService _isuExtraService = new IsuExtraService();

    [Fact]
    public void AddStudentToGroup_GroupContainsStudent()
    {
        StudentsGroup group = _isuExtraService.AddGroup("M32051");
        ItmoStudent student = _isuExtraService.AddStudent("name", group);
        Assert.Contains(student, group.StudentsInGroup);
    }

    [Fact]
    public void AddScheduleToGroup_GroupHasSchedule()
    {
        StudentsGroup group = _isuExtraService.AddGroup("M32051");
        var lesson1 = new Lesson(
            "prob",
            new TimeOnly(10, 0, 0),
            new TimeOnly(11, 30, 0),
            group,
            "Limar",
            100);
        var lesson2 = new Lesson(
            "maths",
            new TimeOnly(11, 40, 0),
            new TimeOnly(13, 10, 0),
            group,
            "Lapin",
            100);
        var schedule = new Schedule();
        schedule.AddLesson(lesson1);
        schedule.AddLesson(lesson2);
        _isuExtraService.AddGroupsSchedule(group, schedule);
        Assert.Equal(lesson1, group.StudentSchedule.Lessons[0]);
        Assert.Equal(lesson2.Teacher, group.StudentSchedule.Lessons[1].Teacher);
    }

    [Fact]
    public void AddOgnpToStudent_StudentHasOgnp()
    {
        StudentsGroup group = _isuExtraService.AddGroup("M32051");
        ItmoStudent student = _isuExtraService.AddStudent("name", group);
        var faculty = new Megafaculty(new GroupName("B32019"));
        Ognp ognp = _isuExtraService.AddOgnp("ognp", faculty);
        var lesson1 = new Lesson(
            "lesson",
            new TimeOnly(10, 0, 0),
            new TimeOnly(11, 30, 0),
            group,
            "somebody",
            100);
        var schedule = new Schedule();
        schedule.AddLesson(lesson1);
        OgnpFlow flow = _isuExtraService.AddOgnpFlow("something", schedule, faculty, ognp);
        _isuExtraService.SignUpStudentToOgnpFlow(student, ognp, flow);
        Assert.Equal(student.StudentOgnp, ognp);
        Assert.Equal(student.Flows[0], flow);
        Assert.Equal("KTU", faculty.MegafacultyName);
    }

    [Fact]
    public void AddOgnpToStudent_StudentDeleteOgnp()
    {
        StudentsGroup group = _isuExtraService.AddGroup("M32051");
        ItmoStudent student = _isuExtraService.AddStudent("name", group);
        var faculty = new Megafaculty(new GroupName("B32019"));
        Ognp ognp = _isuExtraService.AddOgnp("ognp", faculty);
        var lesson1 = new Lesson(
            "lesson",
            new TimeOnly(10, 0, 0),
            new TimeOnly(11, 30, 0),
            group,
            "somebody",
            100);
        var schedule = new Schedule();
        schedule.AddLesson(lesson1);
        OgnpFlow flow = _isuExtraService.AddOgnpFlow("something", schedule, faculty, ognp);
        _isuExtraService.SignUpStudentToOgnpFlow(student, ognp, flow);
        _isuExtraService.DeleteStudentOgnp(student, ognp);
        Assert.Null(student.StudentOgnp);
    }

    [Fact]
    public void GetStudentsWithoutOgnp()
    {
        StudentsGroup group = _isuExtraService.AddGroup("M32051");
        ItmoStudent student1 = _isuExtraService.AddStudent("name", group);
        ItmoStudent student2 = _isuExtraService.AddStudent("name2", group);
        var faculty = new Megafaculty(new GroupName("B32019"));
        Ognp ognp = _isuExtraService.AddOgnp("ognp", faculty);
        var lesson1 = new Lesson(
            "lesson",
            new TimeOnly(10, 0, 0),
            new TimeOnly(11, 30, 0),
            group,
            "somebody",
            100);
        var schedule = new Schedule();
        schedule.AddLesson(lesson1);
        OgnpFlow flow = _isuExtraService.AddOgnpFlow("something", schedule, faculty, ognp);
        _isuExtraService.SignUpStudentToOgnpFlow(student1, ognp, flow);
        Assert.Equal(student2, _isuExtraService.GetStudentsWithoutOgnp()[0]);
        Assert.Equal(student1, flow.StudentsInFlow[0]);
    }

    [Fact]
    public void CheckIntersectsSchedule()
    {
        StudentsGroup group = _isuExtraService.AddGroup("M32051");
        ItmoStudent student = _isuExtraService.AddStudent("name", group);
        var lesson1 = new Lesson(
            "prob",
            new TimeOnly(10, 0, 0),
            new TimeOnly(11, 30, 0),
            group,
            "Limar",
            100);
        var lesson2 = new Lesson(
            "maths",
            new TimeOnly(11, 40, 0),
            new TimeOnly(13, 10, 0),
            group,
            "Lapin",
            100);
        var lesson3 = new Lesson(
            "phys",
            new TimeOnly(11, 00, 0),
            new TimeOnly(12, 30, 0),
            group,
            "Zinchik",
            100);
        var schedule1 = new Schedule();
        schedule1.AddLesson(lesson1);
        schedule1.AddLesson(lesson2);
        _isuExtraService.AddGroupsSchedule(group, schedule1);
        var schedule2 = new Schedule();
        schedule2.AddLesson(lesson3);

        var faculty = new Megafaculty(new GroupName("B32019"));
        Ognp ognp = _isuExtraService.AddOgnp("ognp", faculty);
        OgnpFlow flow = _isuExtraService.AddOgnpFlow("something", schedule2, faculty, ognp);
        Assert.Throws<ItmoStudentException>(() =>
        {
            _isuExtraService.SignUpStudentToOgnpFlow(student, ognp, flow);
        });
    }
}