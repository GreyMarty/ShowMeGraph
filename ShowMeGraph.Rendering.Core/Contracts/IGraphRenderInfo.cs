namespace ShowMeGraph.Contracts;

public interface IGraphRenderInfo
{
    public IEnumerable<IRenderNode> Nodes { get; }
    public IEnumerable<IRenderEdge<IRenderNode>> Edges { get; }

    public bool Directed { get; }

    public IRenderEdge<IRenderNode>? Edge(IRenderNode source, IRenderNode target);
}
