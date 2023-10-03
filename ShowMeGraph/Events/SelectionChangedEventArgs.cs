using ShowMeGraph.Contracts;

namespace ShowMeGraph.Events;

public class SelectionChangedEventArgs : EventArgs
{
    public ISelectable? Selected { get; set; }
    public ISelectable[] Deselected { get; set; } = Array.Empty<ISelectable>();
}
