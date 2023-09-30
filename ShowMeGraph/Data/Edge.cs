using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class Edge : IRenderEdge<Node>, IRenderEdge<IRenderNode>, QuikGraph.IEdge<Node>
{
    public Node Source { get; set; } = default!;
    public Node Target { get; set; } = default!;
    IRenderNode IEdge<IRenderNode>.Source => Source;
    IRenderNode IEdge<IRenderNode>.Target => Target;
    
    public int Weight { get; set; }

    public string? Color { get; set; }
    public string? Text => Weight.ToString();
}
