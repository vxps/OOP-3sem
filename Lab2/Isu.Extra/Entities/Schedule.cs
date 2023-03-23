namespace Isu.Extra.Entities;

public class Schedule
{
    private List<Lesson> _lessons;

    public Schedule()
    {
        _lessons = new List<Lesson>();
    }

    public Schedule(List<Lesson> lessons)
    {
        ArgumentNullException.ThrowIfNull(lessons);

        _lessons = lessons;
    }

    public IReadOnlyList<Lesson> Lessons => _lessons;

    public Lesson AddLesson(Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(lesson);

        _lessons.Add(lesson);

        return lesson;
    }

    public bool CheckIntersect(Lesson other)
    {
        return _lessons.All(lesson => !other.Start.IsBetween(lesson.Start, lesson.End) && !other.End.IsBetween(lesson.Start, lesson.End));
    }
}