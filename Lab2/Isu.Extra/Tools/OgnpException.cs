namespace Isu.Extra.Tools;

public class OgnpException : Exception
{
    public OgnpException(string message)
        : base(message) { }

    public static OgnpException InvalidOgnpNameException()
    {
        throw new OgnpException("ognp name can't be empty or null");
    }
}