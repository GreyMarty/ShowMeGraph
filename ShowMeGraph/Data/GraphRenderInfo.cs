using QuikGraph;
using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class GraphRenderInfo<TGraph> : IGraphRenderInfo
    where TGraph : IEdgeListGraph<Node, Edge>
{
    private readonly TGraph _graph;

    public GraphRenderInfo(TGraph graph)
    {
        _graph = graph;
    }

    public IEnumerable<IRenderNode> Nodes => _graph.Vertices;
    public IEnumerable<IRenderEdge<IRenderNode>> Edges => _graph.Edges;

    public bool Directed => _graph.IsDirected;

    public IRenderEdge<IRenderNode>? Edge(IRenderNode source, IRenderNode target) 
    {
        if ((_graph as IImplicitUndirectedGraph<Node, Edge>)?.TryGetEdge((Node)source, (Node)target, out var edge) == true)
        {
            return edge;
        }

        if ((_graph as IIncidenceGraph<Node, Edge>)?.TryGetEdge((Node)source, (Node)target, out edge) == true)
        {
            return edge;
        }

        return _graph.Edges.FirstOrDefault(e => e.Source == source && e.Target == target);
    }
}
