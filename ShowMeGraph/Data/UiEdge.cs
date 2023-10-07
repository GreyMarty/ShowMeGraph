using ShowMeGraph.Contracts;
using ShowMeGraph.Data.Constants;

namespace ShowMeGraph.Data;

public class UiEdge : IRenderEdge<UiVertex>, IRenderEdge<IRenderNode>, QuikGraph.IEdge<UiVertex>, ISelectable, IHoverable
{
    public UiVertex Source { get; set; }
    public UiVertex Target { get; set; }
    IRenderNode IEdge<IRenderNode>.Source => Source;
    IRenderNode IEdge<IRenderNode>.Target => Target;

    public int Weight { get; set; }

    public string? DisplayedColor => (Selected, Hovered) switch
    {
        (true, _) => CustomColors.SelectedStrokeColor,
        (false, true) => CustomColors.HoveredStrokeColor,
        _ => Color
    };

    public string? DisplayedText => Text ?? Weight.ToString();

    public string? Color { get; set; }
    public string? Text { get; set; }

    public bool Selected { get; set; }
    public bool Hovered { get; set; }

    public UiEdge(UiVertex source, UiVertex target)
    {
        Source = source;
        Target = target;
    }
}
