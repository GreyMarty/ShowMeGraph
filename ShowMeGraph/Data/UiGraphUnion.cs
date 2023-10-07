using QuikGraph;
using ShowMeGraph.Contracts;
using SkiaSharp;

namespace ShowMeGraph.Data;

public class UiGraphUnion<TDirectedGraph, TUndirectedGraph> : IUiGraph<UiVertex, UiEdge>
    where TDirectedGraph : IUiGraph<UiVertex, UiEdge>
    where TUndirectedGraph : IUiGraph<UiVertex, UiEdge>
{
    private IUiGraph<UiVertex, UiEdge> _value;

    public bool IsDirected
    {
        get => _value.IsDirected;
        set
        {
            if (value == _value.IsDirected)
            {
                return;
            }

            var oldValue = _value;

            if (value)
            {
                _value = (TDirectedGraph)Activator.CreateInstance(typeof(TDirectedGraph), (object)AllowParallelEdges)!;
            }
            else
            {
                _value = (TUndirectedGraph)Activator.CreateInstance(typeof(TUndirectedGraph), (object)AllowParallelEdges)!;
            }

            _value.AddVertexRange(oldValue.Vertices);
            _value.AddEdgeRange(oldValue.Edges);

            _value.VertexAdded += e => VertexAdded?.Invoke(e);
            _value.VertexRemoved += e => VertexRemoved?.Invoke(e);
            _value.EdgeAdded += e => EdgeAdded?.Invoke(e);
            _value.EdgeRemoved += e => EdgeRemoved?.Invoke(e);
        }
    }

    public bool AllowParallelEdges => _value.AllowParallelEdges;

    public bool IsVerticesEmpty => _value.IsVerticesEmpty;

    public int VertexCount => _value.VertexCount;

    public IEnumerable<UiVertex> Vertices => _value.Vertices;

    public bool IsEdgesEmpty => _value.IsEdgesEmpty;

    public int EdgeCount => _value.EdgeCount;

    public IEnumerable<UiEdge> Edges => _value.Edges;

    public event VertexAction<UiVertex> VertexAdded;
    public event VertexAction<UiVertex> VertexRemoved;
    public event EdgeAction<UiVertex, UiEdge> EdgeAdded;
    public event EdgeAction<UiVertex, UiEdge> EdgeRemoved;

    public UiGraphUnion(bool directed, bool allowParallelEdges = false)
    {
        if (directed)
        {
            _value = (TDirectedGraph)Activator.CreateInstance(typeof(TDirectedGraph), (object)allowParallelEdges)!;
        }
        else
        {
            _value = (TUndirectedGraph)Activator.CreateInstance(typeof(TUndirectedGraph), (object)allowParallelEdges)!;
        }

        _value.VertexAdded += e => VertexAdded?.Invoke(e);
        _value.VertexRemoved += e => VertexRemoved?.Invoke(e);
        _value.EdgeAdded += e => EdgeAdded?.Invoke(e);
        _value.EdgeRemoved += e => EdgeRemoved?.Invoke(e);
    }

    public static implicit operator TDirectedGraph(UiGraphUnion<TDirectedGraph, TUndirectedGraph> union) => (TDirectedGraph)union._value;
    public static implicit operator TUndirectedGraph(UiGraphUnion<TDirectedGraph, TUndirectedGraph> union) => (TUndirectedGraph)union._value;

    public void SetValue(IUiGraph<UiVertex, UiEdge> value)
    {
        if (value is not TDirectedGraph && value is not TUndirectedGraph)
        {
            throw new NotSupportedException();
        }

        _value = value;

        _value.VertexAdded += e => VertexAdded?.Invoke(e);
        _value.VertexRemoved += e => VertexRemoved?.Invoke(e);
        _value.EdgeAdded += e => EdgeAdded?.Invoke(e);
        _value.EdgeRemoved += e => EdgeRemoved?.Invoke(e);
    }

    public void Match(Action<TDirectedGraph> directed, Action<TUndirectedGraph> undirected)
    {
        if (IsDirected)
        {
            directed((TDirectedGraph)_value);
        }
        else
        {
            undirected((TUndirectedGraph)_value);
        }
    }

    public bool AddEdge(UiEdge edge) => _value.AddEdge(edge);

    public int AddEdgeRange(IEnumerable<UiEdge> edges) => _value.AddEdgeRange(edges);

    public bool AddVertex(UiVertex vertex) => _value.AddVertex(vertex);

    public int AddVertexRange(IEnumerable<UiVertex> vertices) => _value.AddVertexRange(vertices);

    public bool AddVerticesAndEdge(UiEdge edge) => _value.AddVerticesAndEdge(edge);

    public int AddVerticesAndEdgeRange(IEnumerable<UiEdge> edges) => _value.AddVerticesAndEdgeRange(edges);

    public IEnumerable<UiVertex> AdjacentVertices(UiVertex vertex) => _value.AdjacentVertices(vertex);

    public void Clear() => _value.Clear();

    public bool ContainsEdge(UiEdge edge) => _value.ContainsEdge(edge);

    public bool ContainsEdge(UiVertex source, UiVertex target) => _value.ContainsEdge(source, target);

    public bool ContainsVertex(UiVertex vertex) => _value.ContainsVertex(vertex);

    public IEnumerable<UiEdge> OutEdges(UiVertex vertex) => _value.OutEdges(vertex);

    public bool RemoveEdge(UiEdge edge) => _value.RemoveEdge(edge);

    public int RemoveEdgeIf(EdgePredicate<UiVertex, UiEdge> predicate) => _value.RemoveEdgeIf(predicate);

    public bool RemoveVertex(UiVertex vertex) => _value.RemoveVertex(vertex);

    public int RemoveVertexIf(VertexPredicate<UiVertex> predicate) => _value.RemoveVertexIf(predicate);

    public bool TryGetEdge(UiVertex source, UiVertex target, out UiEdge edge) => _value.TryGetEdge(source, target, out edge);
}
