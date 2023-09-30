using ShowMeGraph.Contracts;

namespace ShowMeGraph.Data;

public class Node : IRenderNode
{
    public string? Color { get; set; }
    public string? StrokeColor { get; set; }
    public string? Text { get; set; }

    public bool Fixed { get; set; }
    public Vector2F Position { get; set; }
}
