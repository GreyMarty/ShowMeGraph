using OpenTK.Graphics.OpenGL;
using ShowMeGraph.Contracts;
using ShowMeGraph.Data;
using SkiaSharp;

namespace ShowMeGraph.Rendering.SkiaSharp;

public class SKEdgeTextRenderer : IRender<SKRenderTarget, IGraphRenderInfo>
{
    private readonly SKRenderingOptions _options;

    public SKEdgeTextRenderer(SKRenderingOptions options)
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

        foreach (var edge in info.Edges)
        {
            var text = edge.DisplayedText;

            if (string.IsNullOrEmpty(text))
            {
                continue;
            }

            var srcPosition = edge.Source.Position;
            var destPosition = edge.Target.Position;

            var hasReturnEdge = info.IsDirected && info.Edge(edge.Target, edge.Source) is not null;

            var direction = (destPosition - srcPosition).Normalized();
            var normal = new Vector2F(-direction.Y, direction.X);

            var normalOffset = hasReturnEdge ? -(SKRenderingConstans.EdgeTextOffset + SKRenderingConstans.ParallelEdgeOffset) : -SKRenderingConstans.EdgeTextOffset;

            var position = (srcPosition + destPosition) / 2 + normal * normalOffset;
            position *= target.PixelsPerUnit;

            font.MeasureText(text.Select(x => (ushort)x).ToArray(), out var rect);
            var textBlob = SKTextBlob.Create(text, font, new(-rect.MidX, -rect.MidY));

            canvas.Save();

            canvas.Translate(position.X, position.Y);
            canvas.RotateRadians(MathF.Atan2(direction.Y, direction.X));

            if (normal.Y < 0)
            {
                canvas.RotateDegrees(180);
            }

            canvas.DrawText(textBlob, 0, 0, paint);

            canvas.Restore();
        }
    }
}

