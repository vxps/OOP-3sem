namespace Isu.Extra.Tools;

public class MegafacultyException : Exception
{
    public MegafacultyException(string message)
        : base(message) { }

    public static MegafacultyException InvalidMegafacultyNameException()
    {
        throw new MegafacultyException("no such megafaculty");
    }
}