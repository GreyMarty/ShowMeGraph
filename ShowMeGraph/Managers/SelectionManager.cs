using Microsoft.AspNetCore.Components.Web;
using ShowMeGraph.Blazor.Events;
using ShowMeGraph.Contracts;
using ShowMeGraph.Events;
using ShowMeGraph.Pages;

namespace ShowMeGraph.Managers;

public class SelectionManager
{
    private readonly IndexViewModel _index;

    private bool _aggregateEvents = true;
    private List<SelectionChangedEventArgs> _events = new();

    public HashSet<ISelectable> SelectedObjects { get; } = new();
    public bool Enabled { get; set; } = true;

    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

    public SelectionManager(IndexViewModel index)
    {
        _index = index;
        _index.NodeClicked += Index_NodeClicked;
        _index.EdgeClicked += Index_EdgeClicked;
        _index.WhitespaceClicked += Index_WhitespaceClicked;
    }

    ~SelectionManager() 
    {
        _index.NodeClicked -= Index_NodeClicked;
        _index.EdgeClicked -= Index_EdgeClicked;
        _index.WhitespaceClicked -= Index_WhitespaceClicked;
    }

    public void Select(ISelectable target)
    {
        if (!Enabled)
        {
            return;
        }

        target.Selected = true;
        SelectedObjects.Add(target);

        if (!_aggregateEvents)
        {
            SelectionChanged?.Invoke(this, new() { Selected = target });
        }
        else
        {
            _events.Add(new() { Selected = target });
        }
    }

    public void Deselect(ISelectable target)
    {
        if (!Enabled)
        {
            return;
        }

        if (!SelectedObjects.Contains(target))
        {
            return;
        }

        target.Selected = false;
        SelectedObjects.Remove(target);

        if (!_aggregateEvents)
        {
            SelectionChanged?.Invoke(this, new() { Deselected = new[] { target } });
        }
        else 
        {
            _events.Add(new() { Deselected = new[] { target } });
        }
    }

    public void SwitchSelection(ISelectable target)
    {
        if (!Enabled)
        {
            return;
        }

        if (SelectedObjects.Contains(target))
        {
            Deselect(target);
            return;
        }

        Select(target);
    }

    public void ClearSelection()
    {
        if (!Enabled)
        {
            return;
        }

        foreach (var selectedObject in SelectedObjects)
        {
            selectedObject.Selected = false;
        }

        var selectedObjects = SelectedObjects.ToArray();
        SelectedObjects.Clear();

        if (!_aggregateEvents)
        {
            SelectionChanged?.Invoke(this, new() { Deselected = selectedObjects });
        }
        else
        {
            _events.Add(new() { Deselected = selectedObjects });
        }
    }

    private bool MultiSelectEnabled(MouseEventArgs? e) => e?.ShiftKey == true;

    private SelectionChangedEventArgs AggregateEvents() 
    {
        var args = new SelectionChangedEventArgs
        {
            Selected = _events.Select(e => e.Selected).FirstOrDefault(x => x is not null),
            Deselected = _events.SelectMany(e => e.Deselected).ToArray()
        };

        _events.Clear();
        return args;
    }

    private void Index_NodeClicked(object? sender, NodeEventArgs e)
    {
        var mouseArgs = e.SenderEventArgs as MouseEventArgs;

        var node = e.Node as ISelectable;
        
        if (node is null)
        {
            return;
        }

        _aggregateEvents = true;

        if (!MultiSelectEnabled(mouseArgs))
        {
            ClearSelection();
        }

        SwitchSelection(node);

        _aggregateEvents = false;
        SelectionChanged?.Invoke(this, AggregateEvents());
    }

    private void Index_EdgeClicked(object? sender, EdgeEventArgs e)
    {
        var mouseArgs = e.SenderEventArgs as MouseEventArgs;

        var edge = e.Edge as ISelectable;

        if (edge is null)
        {
            return;
        }

        _aggregateEvents = true;

        if (!MultiSelectEnabled(mouseArgs))
        {
            ClearSelection();
        }

        SwitchSelection(edge);

        _aggregateEvents = false;
        SelectionChanged?.Invoke(this, AggregateEvents());
    }

    private void Index_WhitespaceClicked(object? sender, LocalMouseEventArgs e)
    {
        if (MultiSelectEnabled(e.MouseEventArgs))
        {
            return;
        }

        ClearSelection();
    }
}
