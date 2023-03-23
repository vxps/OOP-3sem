using Isu.Extra.Entities;
using Isu.Extra.Tools;

namespace Isu.Extra.Models;

public class Ognp
{
    private const int MaxFlows = 2;
    private List<OgnpFlow> _flows;

    public Ognp()
    {
        Name = null;
        OgnpMegafaculty = null;
        _flows = new List<OgnpFlow>();
    }

    public Ognp(string name, Megafaculty megafaculty)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw OgnpException.InvalidOgnpNameException();
        ArgumentNullException.ThrowIfNull(megafaculty);

        Name = name;
        OgnpMegafaculty = megafaculty;
        _flows = new List<OgnpFlow>();
    }

    public string? Name { get; }
    public Megafaculty? OgnpMegafaculty { get; }
    public IReadOnlyList<OgnpFlow> Flows => _flows;

    public bool CheckFlowFull()
        => _flows.Count >= MaxFlows;

    public bool CheckFlowExist(OgnpFlow flow)
        => _flows.Any(other => other.Name == flow.Name && other.IdCounter == flow.IdCounter);

    public OgnpFlow AddFlow(OgnpFlow flow)
    {
        ArgumentNullException.ThrowIfNull(flow);
        if (CheckFlowFull())
            throw new OgnpFlowException("flow is full");
        if (CheckFlowExist(flow))
            throw new OgnpFlowException("this flow already exist");

        _flows.Add(flow);
        return flow;
    }
}