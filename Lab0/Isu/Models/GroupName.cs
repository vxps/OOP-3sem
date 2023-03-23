using Isu.Tools;

namespace Isu.Models;

public class GroupName
{
    private const int GroupNameLength = 6;
    public GroupName(string name)
    {
        if (!char.IsLetter(name[0]))
             throw new InvalidGroupnameException("Wrong group name");
        if (name[1..name.Length].Any(letter => !char.IsNumber(letter)))
            throw new InvalidGroupnameException("Wrong group name");
        if (name.Length != GroupNameLength)
            throw new InvalidGroupnameException("wrong group name");

        Name = name;
    }

    public string Name { get; }

    public bool Equals(GroupName? other)
        => other is not null && other.Name == this.Name;
}