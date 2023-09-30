namespace ShowMeGraph.Contracts;

public interface IGraphLayoutInfo
{
    public IEnumerable<ILayoutNode> Nodes { get; }

    public bool AreAdjacent(ILayoutNode a, ILayoutNode b);
    public IEnumerable<ILayoutNode> AdjacentNodes(ILayoutNode node);
}
