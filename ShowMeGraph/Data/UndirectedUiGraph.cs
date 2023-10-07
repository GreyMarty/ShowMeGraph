using QuikGraph;
using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class UndirectedUiGraph : UndirectedGraph<UiVertex, UiEdge>, IUiGraph<UiVertex, UiEdge>
{
    public UndirectedUiGraph(bool allowParallelEdges = false) : base(allowParallelEdges)
    {
        
    }

    public UndirectedUiGraph(UndirectedGraph<UiVertex, UiEdge> graph) : this(graph.AllowParallelEdges)
    {
        AddVertexRange(graph.Vertices);
        AddEdgeRange(graph.Edges);
    }

    public IEnumerable<UiEdge> OutEdges(UiVertex vertex)
    {
        return Edges.Where(x => x.Source == vertex || x.Target == vertex);
    }
}
