using QuikGraph;
using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class GraphLayoutInfo : IGraphLayoutInfo
{

    private readonly IUiGraph<UiVertex, UiEdge> _graph;

    public GraphLayoutInfo(IUiGraph<UiVertex, UiEdge> graph)
    {
        _graph = graph;
    }

    public IEnumerable<ILayoutNode> Vertices => _graph.Vertices;

    public IEnumerable<ILayoutNode> AdjacentVertices(ILayoutNode node)
    {
        var outNodes = _graph.Edges
            .Where(e => e.Source == node)
            .Select(e => e.Target);

        var inNodes = _graph.Edges
            .Where(e => e.Target == node)
            .Select(e => e.Source);

        return outNodes.Concat(inNodes);
    }

    public bool AreAdjacent(ILayoutNode a, ILayoutNode b) => _graph.ContainsEdge((UiVertex)a, (UiVertex)b);
}
