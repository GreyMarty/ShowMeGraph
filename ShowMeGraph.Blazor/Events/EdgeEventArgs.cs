using ShowMeGraph.Contracts;

namespace ShowMeGraph.Blazor.Events;

public class EdgeEventArgs : EventArgs
{
    public IRenderEdge<IRenderNode> Edge { get; }
    public EventArgs? SenderEventArgs { get; }

    public EdgeEventArgs(IRenderEdge<IRenderNode> edge, EventArgs? senderEventArgs = null)
    {
        Edge = edge;
        SenderEventArgs = senderEventArgs;
    }
}
