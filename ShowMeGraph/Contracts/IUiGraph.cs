using QuikGraph;

namespace ShowMeGraph.Contracts;

public interface IUiGraph<TVertex, TEdge> : IMutableVertexAndEdgeSet<TVertex, TEdge>
    where TEdge : QuikGraph.IEdge<TVertex>
{
    public IEnumerable<TVertex> AdjacentVertices(TVertex vertex);
    public IEnumerable<TEdge> OutEdges(TVertex vertex);
    public bool ContainsEdge(TVertex source, TVertex target);
    public bool TryGetEdge(TVertex source, TVertex target, out TEdge edge);
}
