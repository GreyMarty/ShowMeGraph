using QuikGraph;
using ShowMeGraph.Contracts;
using System.Xml;

using DirectedGraphSerializer = QuikGraph.Serialization.GraphMLSerializer<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge, QuikGraph.AdjacencyGraph<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge>>;
using UndirectedGraphSerializer = QuikGraph.Serialization.GraphMLSerializer<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge, QuikGraph.UndirectedGraph<ShowMeGraph.Data.UiVertex, ShowMeGraph.Data.UiEdge>>;

namespace ShowMeGraph.Data.Serialization;

public class UiGraphSerializer<TGraph>
    where TGraph : IUiGraph<UiVertex, UiEdge>, IEdgeListGraph<UiVertex, UiEdge>
{
    private readonly object _serializer;

    public UiGraphSerializer()
    {
        if (typeof(TGraph) == typeof(DirectedUiGraph))
        {
            _serializer = new DirectedGraphSerializer();
            return;
        }

        if (typeof(TGraph) == typeof(UndirectedUiGraph))
        {
            _serializer = new UndirectedGraphSerializer();
            return;
        }

        throw new NotSupportedException($"{typeof(TGraph)} is not supported.");
    }

    public void Serialize(XmlWriter writer, TGraph graph, VertexIdentity<UiVertex> vertexIdentity, EdgeIdentity<UiVertex, UiEdge> edgeIdentity)
    {
        if (typeof(TGraph) == typeof(DirectedUiGraph))
        {
            ((DirectedGraphSerializer)_serializer).Serialize(writer, (graph as AdjacencyGraph<UiVertex, UiEdge>)!, vertexIdentity, edgeIdentity);
            return;
        }

        ((UndirectedGraphSerializer)_serializer).Serialize(writer, (graph as UndirectedGraph<UiVertex, UiEdge>)!, vertexIdentity, edgeIdentity);
    }
}
