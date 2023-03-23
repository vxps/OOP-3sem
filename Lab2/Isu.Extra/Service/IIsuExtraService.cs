using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;

namespace Isu.Extra.Service;

public interface IIsuExtraService
{
    ItmoStudent AddStudent(string name, StudentsGroup group);
    StudentsGroup AddGroup(string name);
    Schedule AddGroupsSchedule(StudentsGroup group, Schedule schedule);
    Ognp AddOgnp(string name, Megafaculty megafaculty);
    OgnpFlow AddOgnpFlow(string name, Schedule schedule, Megafaculty megafaculty, Ognp ognp);
    ItmoStudent SignUpStudentToOgnpFlow(ItmoStudent student, Ognp ognp, OgnpFlow flow);
    ItmoStudent DeleteStudentFlow(ItmoStudent student, OgnpFlow flow);
    ItmoStudent DeleteStudentOgnp(ItmoStudent student, Ognp ognp);
    IReadOnlyList<OgnpFlow> GetFlowsInOgnp(Ognp ognp);
    IReadOnlyList<ItmoStudent> GetStudentsFromOgnp(Ognp ognp);
    IReadOnlyList<ItmoStudent> GetStudentsWithoutOgnp();
}