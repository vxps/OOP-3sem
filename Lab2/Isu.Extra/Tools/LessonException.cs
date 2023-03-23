namespace Isu.Extra.Tools;

public class LessonException : Exception
{
    public LessonException(string message)
        : base(message) { }

    public static LessonException InvalidNameException()
    {
        throw new LessonException("name is null or empty");
    }

    public static LessonException InvalidTeacherName()
    {
        throw new LessonException("teacher's name is null or empty");
    }

    public static LessonException InvalidClassroomName()
    {
        throw new LessonException("classroom name is null or empty");
    }
}