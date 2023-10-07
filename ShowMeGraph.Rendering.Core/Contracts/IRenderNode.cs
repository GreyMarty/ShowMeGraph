namespace ShowMeGraph.Contracts;

public interface IRenderNode : ILayoutNode
{
    public string? Color { get; }
    public string? DisplayedStrokeColor { get; }
    public string? Text { get; }
}
