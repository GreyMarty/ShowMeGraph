using ShowMeGraph.Blazor.Events;
using ShowMeGraph.Data;
using ShowMeGraph.Pages;
using ShowMeGraph.Shared;

namespace ShowMeGraph.Tools;

public class NodeCreationTool : ITool
{
    private readonly IndexViewModel _index;

    public string Icon => CustomIcons.Outlined.AddCircle;
    public bool Selectable => true;

    public NodeCreationTool(IndexViewModel viewModel)
    {
        _index = viewModel;
    }

    public void Activate()
    {
        _index.AllowPanning = false;
        _index.AllowDragging = false;
        _index.SelectionManager.Enabled = false;
        _index.HoverManager.Enabled = false;

        _index.AnimationManager.Stop();

        _index.WhitespaceClicked += Index_WhitespaceClicked;
    }

    public void Deactivate()
    {
        _index.WhitespaceClicked -= Index_WhitespaceClicked;
    }

    private void Index_WhitespaceClicked(object? sender, LocalMouseEventArgs e)
    {
        var node = new VisNode();
        node.Position = e.LocalPosition;

        _index.Graph.Value.AddVertex(node);
    }
}
