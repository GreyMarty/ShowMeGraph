using QuikGraph;
using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class GraphRenderInfo : IGraphRenderInfo
{
    private readonly VisGraph _graph;

    public GraphRenderInfo(VisGraph graph)
    {
        _graph = graph;
    }

    public IEnumerable<IRenderNode> Nodes => _graph.Value.Vertices;
    public IEnumerable<IRenderEdge<IRenderNode>> Edges => _graph.Value.Edges;

    public bool Directed => _graph.IsDirected;

    public IRenderEdge<IRenderNode>? Edge(IRenderNode source, IRenderNode target) 
    {
        if ((_graph.Value as IImplicitUndirectedGraph<VisNode, VisEdge>)?.TryGetEdge((VisNode)source, (VisNode)target, out var edge) == true)
        {
            return edge;
        }

        if ((_graph.Value as IIncidenceGraph<VisNode, VisEdge>)?.TryGetEdge((VisNode)source, (VisNode)target, out edge) == true)
        {
            return edge;
        }

        return _graph.Value.Edges.FirstOrDefault(e => e.Source == source && e.Target == target);
    }
}
