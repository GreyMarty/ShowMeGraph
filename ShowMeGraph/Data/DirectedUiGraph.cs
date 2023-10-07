using QuikGraph;
using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class DirectedUiGraph : AdjacencyGraph<UiVertex, UiEdge>, IUiGraph<UiVertex, UiEdge>
{
    public DirectedUiGraph(bool allowParallelEdges = false) : base(allowParallelEdges)
    {
        
    }

    public DirectedUiGraph(AdjacencyGraph<UiVertex, UiEdge> graph) : this(graph.AllowParallelEdges)
    {
        AddVertexRange(graph.Vertices);
        AddEdgeRange(graph.Edges);
    }

    public IEnumerable<UiVertex> AdjacentVertices(UiVertex vertex)
    {
        return OutEdges(vertex).Select(x => x.Target);
    }
}
