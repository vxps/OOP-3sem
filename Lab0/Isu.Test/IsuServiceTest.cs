using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using Xunit;

namespace Isu.Test;

public class IsuService
{
    private IIsuService _isuService = new Services.IsuService();
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = _isuService.AddGroup(new GroupName("M32051"));
        Student student = _isuService.AddStudent(group, "Abc");
        Assert.Contains(student, group.StudentsInGroup);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        Assert.Throws<InvalidGroupCountException>(() =>
        {
            Group group = _isuService.AddGroup(new GroupName("M32061"));
            for (int i = 0; i < group.MaxOfStudents + 1; i++)
            {
                _isuService.AddStudent(group, "somebody" + i.ToString());
            }

            _isuService.AddStudent(group, "rrr");
        });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<InvalidGroupnameException>(() =>
        {
            Group group1 = _isuService.AddGroup(new GroupName("M3i051"));
            Group group2 = _isuService.AddGroup(new GroupName("032051"));
        });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group newGroup = _isuService.AddGroup(new GroupName("M32041"));
        Group group = _isuService.AddGroup(new GroupName("P33071"));
        Student student = _isuService.AddStudent(group, "somebody");
        _isuService.ChangeStudentGroup(student, newGroup);
    }
}