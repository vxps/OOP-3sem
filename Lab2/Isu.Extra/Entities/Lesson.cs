using Isu.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class Lesson
{
    private const int MinClassroomNumber = 0;
    private const int LessonDurationInMinutes = 90;
    public Lesson(string name, TimeOnly start, TimeOnly end, StudentsGroup group, string teacher, int classroom)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw LessonException.InvalidNameException();
        if (string.IsNullOrWhiteSpace(teacher))
            throw LessonException.InvalidTeacherName();
        if (classroom < MinClassroomNumber)
            throw new LessonException("classroom must be >0");
        if (end < start)
            throw new LessonException("end time should be > than start time");
        if ((end - start).TotalMinutes != LessonDurationInMinutes)
            throw new LessonException("lesson should lasts 1 hour and 30 minutes");

        ArgumentNullException.ThrowIfNull(start);
        ArgumentNullException.ThrowIfNull(end);
        ArgumentNullException.ThrowIfNull(group);

        Name = name;
        Start = start;
        End = end;
        GroupOfStudents = group;
        Teacher = teacher;
        Classroom = classroom;
    }

    public string Name { get; }
    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    public StudentsGroup GroupOfStudents { get; }
    public string Teacher { get; }
    public int Classroom { get; }
}