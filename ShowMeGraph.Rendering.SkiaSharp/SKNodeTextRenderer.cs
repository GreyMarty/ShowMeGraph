using ShowMeGraph.Contracts;
using SkiaSharp;

namespace ShowMeGraph.Rendering.SkiaSharp;

public class SKNodeTextRenderer : IRender<SKRenderTarget, IGraphRenderInfo>
{
    private readonly SKRenderingOptions _options;

    public SKNodeTextRenderer(SKRenderingOptions options)
    {
        _options = options;
    }

    public void Render(SKRenderTarget target, IGraphRenderInfo info)
    {
        var canvas = target.Canvas;

        var font = new SKFont(SKTypeface.FromFamilyName(_options.FontFamily), _options.FontSize);
        var paint = new SKPaint
        {
            IsAntialias = true,
            Color = SKColor.Parse(_options.StrokeColor)
        };

        foreach (var node in info.Vertices)
        {
            var position = node.Position * target.PixelsPerUnit;

            var text = node.Text;

            if (string.IsNullOrEmpty(text))
            {
                continue;
            }

            font.MeasureText(text.Select(x => (ushort)x).ToArray(), out var rect);
            var textBlob = SKTextBlob.Create(text, font, new(-rect.MidX, -rect.MidY));

            canvas.DrawText(textBlob, position.X, position.Y, paint);
        }
    }
}

