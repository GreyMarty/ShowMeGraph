using ShowMeGraph.Data;
using ShowMeGraph.Events;
using ShowMeGraph.Pages;
using ShowMeGraph.Shared;

namespace ShowMeGraph.Tools;

public class EdgeCreationTool : ITool
{
    private readonly IndexViewModel _index;

    public string Icon => CustomIcons.Outlined.RebaseEdit;
    public bool Selectable => true;

    public EdgeCreationTool(IndexViewModel index)
    {
        _index = index;
    }

    public void Activate()
    {
        _index.AllowDragging = false;
        _index.AllowPanning = true;
        _index.SelectionManager.Enabled = true;
        _index.HoverManager.Enabled = true;
        
        _index.AnimationManager.Stop();

        _index.SelectionManager.SelectionChanged += SelectionManager_SelectionChanged;
    }

    public void Deactivate()
    {
        _index.SelectionManager.SelectionChanged -= SelectionManager_SelectionChanged;
    }

    private void SelectionManager_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var dest = e.Selected as UiVertex;

        if (dest is null)
        {
            return;
        }

        var src = Array.Find(e.Deselected, x => x is UiVertex) as UiVertex;

        if (src is null)
        {
            return;
        }

        _index.Graph.AddEdge(new(src, dest));
    }
}
