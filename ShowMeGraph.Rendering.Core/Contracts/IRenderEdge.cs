namespace ShowMeGraph.Contracts;

public interface IRenderEdge<TNode> : IEdge<TNode>
    where TNode : IRenderNode
{
    public string? DisplayedColor { get; }
    public string? DisplayedText { get; }
}
