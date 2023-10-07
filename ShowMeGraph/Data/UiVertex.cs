using ShowMeGraph.Contracts;
using ShowMeGraph.Data.Constants;

namespace ShowMeGraph.Data;

public class UiVertex : IRenderNode, ISelectable, IHoverable
{
    public string? Color { get; set; }
    public string? DisplayedStrokeColor => (Selected, Hovered) switch
    {
        (true, _) => CustomColors.SelectedStrokeColor,
        (false, true) => CustomColors.HoveredStrokeColor,
        _ => StrokeColor
    };

    public string? StrokeColor { get; set; }

    public string? Text { get; set; }

    public bool Fixed { get; set; }
    public bool Selected { get; set; }
    public bool Hovered { get; set; }

    public Vector2F Position { get; set; }
}
