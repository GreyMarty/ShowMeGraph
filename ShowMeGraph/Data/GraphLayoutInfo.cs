using QuikGraph;
using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class GraphLayoutInfo : IGraphLayoutInfo
{

    private readonly VisGraph _graph;

    public GraphLayoutInfo(VisGraph graph)
    {
        _graph = graph;
    }

    public IEnumerable<ILayoutNode> Nodes => _graph.Value.Vertices;

    public IEnumerable<ILayoutNode> AdjacentNodes(ILayoutNode node)
    {
        var outNodes = _graph.Value.Edges
            .Where(e => e.Source == node)
            .Select(e => e.Target);

        var inNodes = _graph.Value.Edges
            .Where(e => e.Target == node)
            .Select(e => e.Source);

        return outNodes.Concat(inNodes);
    }

    public bool AreAdjacent(ILayoutNode a, ILayoutNode b)
    {
        return
            (_graph.Value as IImplicitUndirectedGraph<VisNode, VisEdge>)?.ContainsEdge((VisNode)a, (VisNode)b) ??
            (_graph.Value as IIncidenceGraph<VisNode, VisEdge>)?.ContainsEdge((VisNode)a, (VisNode)b) ??
            _graph.Value.Edges.Any(e => e.Source == a && e.Target == b);
    }
}
