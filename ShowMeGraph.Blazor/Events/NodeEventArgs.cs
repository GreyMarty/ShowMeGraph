using ShowMeGraph.Contracts;

namespace ShowMeGraph.Blazor.Events;

public class NodeEventArgs : EventArgs
{
    public IRenderNode Node { get; }
    public EventArgs? SenderEventArgs { get; }

    public NodeEventArgs(IRenderNode node, EventArgs? senderEventArgs = null)
    {
        Node = node;
        SenderEventArgs = senderEventArgs;
    }
}
