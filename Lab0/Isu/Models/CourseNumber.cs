using Isu.Entities;
using Isu.Tools;

namespace Isu.Models;

public class CourseNumber
{
    private const int MinCourse = 1;
    private const int MaxCourse = 4;
    private int _courseNumber;
    public CourseNumber(int number)
    {
        if (number is > MaxCourse or < MinCourse)
        {
            throw new InvalidCourseNumberException("Wrong format of course number");
        }

        _courseNumber = number;
    }

    public bool Equals(CourseNumber? other)
        => other is not null && other._courseNumber == _courseNumber;
}