﻿namespace ShowMeGraph.Contracts;

public interface IGraphLayoutInfo
{
    public IEnumerable<ILayoutNode> Vertices { get; }

    public bool AreAdjacent(ILayoutNode a, ILayoutNode b);
    public IEnumerable<ILayoutNode> AdjacentVertices(ILayoutNode node);
}
