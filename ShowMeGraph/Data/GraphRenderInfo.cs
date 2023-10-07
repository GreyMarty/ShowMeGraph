using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class GraphRenderInfo : IGraphRenderInfo
{
    private readonly IUiGraph<UiVertex, UiEdge> _graph;

    public GraphRenderInfo(IUiGraph<UiVertex, UiEdge> graph)
    {
        _graph = graph;
    }

    public IEnumerable<IRenderNode> Vertices => _graph.Vertices;
    public IEnumerable<IRenderEdge<IRenderNode>> Edges => _graph.Edges;

    public bool IsDirected => _graph.IsDirected;

    public IRenderEdge<IRenderNode>? Edge(IRenderNode source, IRenderNode target)
    {
        _graph.TryGetEdge((UiVertex)source, (UiVertex)target, out var edge);
        return edge;
    }
}
