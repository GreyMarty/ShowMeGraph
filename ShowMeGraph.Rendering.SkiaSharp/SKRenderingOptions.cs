using ShowMeGraph.Data;

namespace ShowMeGraph.Rendering.SkiaSharp;

public class SKRenderingOptions
{
    public float FontSize { get; set; }
    public string? FontWeight { get; set; }
    public string FontFamily { get; set; } = default!;
    public string NodeFillColor { get; set; } = default!;
    public float StrokeThickness { get; set; }
    public string StrokeColor { get; set; } = default!;
    public string BackgroundColor { get; set; } = default!;

    public static SKRenderingOptions Default => new()
    {
        FontSize = 14,
        FontWeight = null,
        FontFamily = "Consolas",
        NodeFillColor = "#ffffff",
        StrokeThickness = 2,
        StrokeColor = "#000000",
        BackgroundColor = "#ffffff",
    };
}
