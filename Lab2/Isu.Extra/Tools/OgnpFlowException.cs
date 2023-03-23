using Isu.Extra.Models;

namespace Isu.Extra.Tools;

public class OgnpFlowException : Exception
{
    public OgnpFlowException(string message)
        : base(message) { }

    public static OgnpFlowException InvalidOgnpFlowNameException()
    {
        throw new OgnpFlowException("name is null or empty");
    }

    public static OgnpFlowException InvalidFlowCountException()
    {
        throw new OgnpFlowException("group is full");
    }
}