using ShowMeGraph.Contracts;
using SkiaSharp;

namespace ShowMeGraph.Rendering.SkiaSharp;

public class SKRendererSetup : IRender<SKRenderTarget, IGraphRenderInfo>
{
    private readonly SKRenderingOptions _options;

    public SKRendererSetup(SKRenderingOptions options)
    {
        _options = options;
    }

    public void Render(SKRenderTarget target, IGraphRenderInfo renderable)
    {
        var canvas = target.Canvas;

        canvas.Clear(SKColor.Parse(_options.BackgroundColor));
        canvas.ResetMatrix();

        canvas.Translate(target.Width / 2 + target.Offset.X, target.Height / 2 + target.Offset.Y);
        canvas.Scale(target.Scale);
    }
}
