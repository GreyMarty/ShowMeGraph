namespace ShowMeGraph.Contracts;

public interface IGraphRenderInfo
{
    public IEnumerable<IRenderNode> Vertices { get; }
    public IEnumerable<IRenderEdge<IRenderNode>> Edges { get; }

    public bool IsDirected { get; }

    public IRenderEdge<IRenderNode>? Edge(IRenderNode source, IRenderNode target);
}
