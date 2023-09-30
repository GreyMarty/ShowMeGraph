using ShowMeGraph.Contracts;

namespace ShowMeGraph.Blazor.Events;

public record EdgeEventArgs(IRenderEdge<IRenderNode> Edge);
