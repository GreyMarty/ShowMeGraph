using QuikGraph;
using ShowMeGraph.Contracts;
using System.Xml;

namespace ShowMeGraph.Data.Serialization;

public static class UiGraphUnionSerializer
{
    public static void Serialize<TDirectedGraph, TUndirectedGraph>(StreamWriter writer, UiGraphUnion<TDirectedGraph, TUndirectedGraph> union)
        where TDirectedGraph : IUiGraph<UiVertex, UiEdge>
        where TUndirectedGraph : IUiGraph<UiVertex, UiEdge>
    {
        using var xmlWriter = XmlWriter.Create(writer);

        var vertexIds = new Dictionary<UiVertex, string>();
        var edgeIds = new Dictionary<UiEdge, string>();

        var id = 0;
        foreach (var node in union.Vertices)
        {
            vertexIds[node] = (id++).ToString();
        }

        id = 0;
        foreach (var edge in union.Edges)
        {
            edgeIds[edge] = (id++).ToString();
        }

        var vertexIdentity = new VertexIdentity<UiVertex>(x => vertexIds[x]);
        var edgeIdentity = new EdgeIdentity<UiVertex, UiEdge>(x => edgeIds[x]);

        union.Match(
            d =>
            {
                var serializer = new UiGraphSerializer<TDirectedGraph>();
                serializer.Serialize(xmlWriter, d, vertexIdentity, edgeIdentity);
            },
            u =>
            {
                var serializer = new UiGraphSerializer<TUndirectedGraph>();
                serializer.Serialize(xmlWriter, u, vertexIdentity, edgeIdentity);
            }
        );

        writer.Flush();
    }

    public static void Deserialize<TDirectedGraph, TUndirectedGraph>(StreamReader reader, UiGraphUnion<TDirectedGraph, TUndirectedGraph> union)
        where TDirectedGraph : IUiGraph<UiVertex, UiEdge>
        where TUndirectedGraph : IUiGraph<UiVertex, UiEdge>
    {
        using var xmlReader = XmlReader.Create(reader);

        var nodes = new Dictionary<string, UiVertex>();
        var edges = new Dictionary<string, UiEdge>();

        var vertexFactory = new IdentifiableVertexFactory<UiVertex>(id =>
        {
            if (nodes.ContainsKey(id))
            {
                return nodes[id];
            }

            var node = new UiVertex();
            nodes[id] = node;

            return node;
        });

        var edgeFactory = new IdentifiableEdgeFactory<UiVertex, UiEdge>((src, dest, id) =>
        {
            if (edges.ContainsKey(id))
            {
                return edges[id];
            }

            var edge = new UiEdge(src, dest);
            edges[id] = edge;

            return edge;
        });

        union.Match(
            d =>
            {
                var deserializer = new UiGraphDeserializer<TDirectedGraph>();
                var graph = (TDirectedGraph)Activator.CreateInstance(typeof(TDirectedGraph), (object)union.AllowParallelEdges)!;
                deserializer.Deserialize(xmlReader, graph, vertexFactory, edgeFactory);
                union.SetValue(graph);
            },
            u =>
            {
                var deserializer = new UiGraphDeserializer<TUndirectedGraph>();
                var graph = (TUndirectedGraph)Activator.CreateInstance(typeof(TUndirectedGraph), (object)union.AllowParallelEdges)!;
                deserializer.Deserialize(xmlReader, graph, vertexFactory, edgeFactory);
                union.SetValue(graph);
            }
        );

        reader.Close();
    }
}
