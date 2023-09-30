using ShowMeGraph.Data;

namespace ShowMeGraph.Contracts;

public interface ILayoutNode : INode
{
    public bool Fixed { get; set; }
    public Vector2F Position { get; set; }
}
