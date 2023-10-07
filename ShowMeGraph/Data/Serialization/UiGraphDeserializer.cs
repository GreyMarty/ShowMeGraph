using QuikGraph;
using ShowMeGraph.Contracts;
using System.Xml;

using DirectedGraphDeserializer = QuikGraph.Serialization.GraphMLDeserializer<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge, QuikGraph.AdjacencyGraph<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge>>;
using UndirectedGraphDeserializer = QuikGraph.Serialization.GraphMLDeserializer<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge, QuikGraph.UndirectedGraph<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge>>;

namespace ShowMeGraph.Data.Serialization;

public class UiGraphDeserializer<TGraph>
    where TGraph : IUiGraph<UiVertex, UiEdge>, IEdgeListGraph<UiVertex, UiEdge>
{
    private readonly object _deserializer;

    public UiGraphDeserializer()
    {
        if (typeof(TGraph) == typeof(DirectedUiGraph))
        {
            _deserializer = new DirectedGraphDeserializer();
            return;
        }

        if (typeof(TGraph) == typeof(UndirectedUiGraph))
        {
            _deserializer = new UndirectedGraphDeserializer();
            return;
        }

        throw new NotSupportedException($"{typeof(TGraph)} is not supported.");
    }

    public void Deserialize(XmlReader reader, TGraph graph, IdentifiableVertexFactory<UiVertex> vertexFactory, IdentifiableEdgeFactory<UiVertex, UiEdge> edgeFactory)
    {
        if (typeof(TGraph) == typeof(DirectedUiGraph))
        {
            ((DirectedGraphDeserializer)_deserializer).Deserialize(reader, (graph as AdjacencyGraph<UiVertex, UiEdge>)!, vertexFactory, edgeFactory);
            return;
        }

        ((UndirectedGraphDeserializer)_deserializer).Deserialize(reader, (graph as UndirectedGraph<UiVertex, UiEdge>)!, vertexFactory, edgeFactory);
    }
}
