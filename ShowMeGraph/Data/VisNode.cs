using ShowMeGraph.Contracts;
using ShowMeGraph.Data.Constants;

namespace ShowMeGraph.Data;

public class VisNode : IRenderNode, ISelectable, IHoverable
{
    public string? Color { get; set; }
    public string? StrokeColor => (Selected, Hovered) switch
    {
        (true, _) => CustomColors.SelectedStrokeColor,
        (false, true) => CustomColors.HoveredStrokeColor,
        _ => UserStrokeColor
    };

    public string? UserStrokeColor { get; set; }

    public string? Text { get; set; }

    public bool Fixed { get; set; }
    public bool Selected { get; set; }
    public bool Hovered { get; set; }

    public Vector2F Position { get; set; }
}
