namespace ShowMeGraph.Contracts;

public interface IRenderNode : ILayoutNode
{
    public string? Color { get; }
    public string? StrokeColor { get; }
    public string? Text { get; }
}
