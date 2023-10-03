using QuikGraph;

namespace ShowMeGraph.Data;

public class VisGraph
{
    public IMutableVertexAndEdgeSet<VisNode, VisEdge> Value { get; private set; }
    public Type Type { get; private set; }
    public bool IsDirected => Value.IsDirected;

    public bool Selected { get; set; }

    public VisGraph(bool directed = true)
    {
        if (directed)
        {
            Value = new AdjacencyGraph<VisNode, VisEdge>();
            Type = typeof(AdjacencyGraph<VisNode, VisEdge>);
            return;
        }

        Value = new UndirectedGraph<VisNode, VisEdge>();
        Type = typeof(UndirectedGraph<VisNode, VisEdge>);
    }

    public void SetDirected(bool value)
    {
        if (value == IsDirected)
        {
            return;
        }

        if (value)
        {
            var graph = new AdjacencyGraph<VisNode, VisEdge>(false);
            graph.AddVertexRange(Value.Vertices);
            graph.AddEdgeRange(Value.Edges);
            Value = graph;
        }
        else
        {
            var graph = new UndirectedGraph<VisNode, VisEdge>(false);
            graph.AddVertexRange(Value.Vertices);
            graph.AddEdgeRange(Value.Edges);
            //foreach (var edge in Value.Edges)
            //{
            //    if (graph.ContainsEdge(edge.Target, edge.Source))
            //    {
            //        continue;
            //    }

            //    graph.AddEdge(edge);
            //}

            Value = graph;
        }
    }
}
