using ShowMeGraph.Data;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace ShowMeGraph.Rendering.SkiaSharp;

public class SKRenderTarget
{
    private readonly SKPaintSurfaceEventArgs _paintEventArgs;

    public float Scale { get; set; } = 1.0f;
    public Vector2F Offset { get; set; }

    public float Width => _paintEventArgs.Info.Width;
    public float Height => _paintEventArgs.Info.Height;

    public float Aspect { get; }

    public float PixelsPerUnit { get; }

    public SKCanvas Canvas => _paintEventArgs.Surface.Canvas;

    public SKRenderTarget(SKPaintSurfaceEventArgs paintEventArgs, float unitsPerHeight)
    {
        _paintEventArgs = paintEventArgs;

        Aspect = Width / Height;
        PixelsPerUnit = Height / unitsPerHeight;
    }
}
