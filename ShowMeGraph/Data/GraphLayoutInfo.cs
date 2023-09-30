using QuikGraph;
using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class GraphLayoutInfo<TGraph> : IGraphLayoutInfo
    where TGraph : IEdgeListGraph<Node, Edge>
{

    private readonly TGraph _graph;

    public GraphLayoutInfo(TGraph graph)
    {
        _graph = graph;
    }

    public IEnumerable<ILayoutNode> Nodes => _graph.Vertices;

    public IEnumerable<ILayoutNode> AdjacentNodes(ILayoutNode node)
    {
        var outNodes = _graph.Edges
            .Where(e => e.Source == node)
            .Select(e => e.Target);

        var inNodes = _graph.Edges
            .Where(e => e.Target == node)
            .Select(e => e.Source);

        return outNodes.Union(inNodes);
    }

    public bool AreAdjacent(ILayoutNode a, ILayoutNode b)
    {
        return
            (_graph as IImplicitUndirectedGraph<Node, Edge>)?.ContainsEdge((Node)a, (Node)b) ??
            (_graph as IIncidenceGraph<Node, Edge>)?.ContainsEdge((Node)a, (Node)b) ??
            _graph.Edges.Any(e => e.Source == a && e.Target == b);
    }
}
