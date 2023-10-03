using ShowMeGraph.Pages;
using ShowMeGraph.Shared;

namespace ShowMeGraph.Tools;

public class CursorTool : ITool
{
    private readonly IndexViewModel _index;

    public string Icon => CustomIcons.Outlined.Pointer;
    public bool Selectable => true;

    public CursorTool(IndexViewModel index)
    {
        _index = index;
    }

    public void Activate()
    {
        _index.AllowDragging = true;
        _index.AllowPanning = true;
        _index.SelectionManager.Enabled = true;
        _index.HoverManager.Enabled = true;
    }

    public void Deactivate()
    {
    }
}
