namespace ShowMeGraph.Tools;

public interface ITool
{
    public string Icon { get; }
    public bool Selectable { get; }

    public void Activate();
    public void Deactivate();
}
