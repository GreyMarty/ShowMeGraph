namespace ShowMeGraph.Contracts;

public interface IRenderEdge<TNode> : IEdge<TNode>
    where TNode : IRenderNode
{
    public string? Color { get; }
    public string? Text { get; }
}
