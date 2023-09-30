namespace ShowMeGraph.Contracts;

public interface IEdge<TNode>
    where TNode : INode
{
    public TNode Source { get; }
    public TNode Target { get; }
}
