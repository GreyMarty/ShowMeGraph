using ShowMeGraph.Data;
using ShowMeGraph.Events;
using ShowMeGraph.Pages;
using ShowMeGraph.Shared;

namespace ShowMeGraph.Tools;

public class EraseTool : ITool
{
    private readonly IndexViewModel _index;

    public string Icon => CustomIcons.Outlined.Erase;
    public bool Selectable => true;

    public EraseTool(IndexViewModel index)
    {
        _index = index;
    }

    public void Activate()
    {
        _index.AllowPanning = true;
        _index.AllowDragging = false;
        _index.SelectionManager.Enabled = true;
        _index.HoverManager.Enabled = true;

        _index.SelectionManager.SelectionChanged += SelectionManager_SelectionChanged;
    }

    public void Deactivate()
    {
        _index.SelectionManager.SelectionChanged -= SelectionManager_SelectionChanged;
    }

    private void SelectionManager_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var node = e.Selected as VisNode;

        if (node is not null)
        {
            _index.Graph.Value.RemoveVertex(node);
            return;
        }

        var edge = e.Selected as VisEdge;

        if (edge is not null)
        {
            _index.Graph.Value.RemoveEdge(edge);
        }
    }
}
