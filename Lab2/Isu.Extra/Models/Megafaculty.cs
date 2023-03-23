using Isu.Extra.Tools;
using Isu.Models;

namespace Isu.Extra.Models;

public class Megafaculty
{
    public Megafaculty(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);

        MegafacultyName = GetMegafaculty(groupName.Name);
    }

    public string MegafacultyName { get; }

    public string GetMegafaculty(string name)
    {
        switch (name[0])
        {
            case 'M':
                return "TINT";
            case 'B':
                return "KTU";
            case 'W':
                return "BTINS";
            default:
                throw MegafacultyException.InvalidMegafacultyNameException();
        }
    }
}