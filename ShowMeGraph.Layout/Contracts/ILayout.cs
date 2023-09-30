namespace ShowMeGraph.Contracts;

public interface ILayout
{
    public void SetUp(IGraphLayoutInfo info);
    public void Arrange(IGraphLayoutInfo info);
}
