using ShowMeGraph.Contracts;
using ShowMeGraph.Data.Constants;

namespace ShowMeGraph.Data;

public class VisEdge : IRenderEdge<VisNode>, IRenderEdge<IRenderNode>, QuikGraph.IEdge<VisNode>, ISelectable, IHoverable
{
    public VisNode Source { get; set; }
    public VisNode Target { get; set; }
    IRenderNode IEdge<IRenderNode>.Source => Source;
    IRenderNode IEdge<IRenderNode>.Target => Target;

    public int Weight { get; set; }

    public string? Color => (Selected, Hovered) switch
    {
        (true, _) => CustomColors.SelectedStrokeColor,
        (false, true) => CustomColors.HoveredStrokeColor,
        _ => UserColor
    };

    public string? UserColor { get; set; }

    public string? Text => Weight.ToString();

    public bool Selected { get; set; }
    public bool Hovered { get; set; }

    public VisEdge(VisNode source, VisNode target)
    {
        Source = source;
        Target = target;
    }
}
