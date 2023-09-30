using ShowMeGraph.Contracts;
using ShowMeGraph.Data;
using SkiaSharp;

namespace ShowMeGraph.Rendering.SkiaSharp;

public class SKEdgeRenderer : IRender<SKRenderTarget, IGraphRenderInfo>
{
    private readonly SKRenderingOptions _options;

    public SKEdgeRenderer(SKRenderingOptions options)
    {
        _options = options;
    }

    public void Render(SKRenderTarget target, IGraphRenderInfo info)
    {
        var canvas = target.Canvas;
        var paint = new SKPaint
        {
            IsAntialias = true,
            Color = SKColor.Parse(_options.StrokeColor),
            StrokeWidth = _options.StrokeThickness,
            IsStroke = true
        };

        foreach (var edge in info.Edges)
        {
            var srcPosition = edge.Source.Position;
            var destPosition = edge.Target.Position;

            var hasReturnEdge = info.Directed && info.Edge(edge.Target, edge.Source) is not null;

            var direction = (destPosition - srcPosition).Normalized();
            var normal = new Vector2F(-direction.Y, direction.X);

            var normalOffset = hasReturnEdge ? -SKRenderingConstans.ParallelEdgeOffset : 0f;
            var centerOffset = SKRenderingConstans.NodeRadius * MathF.Cos(MathF.Asin(normalOffset / SKRenderingConstans.NodeRadius));

            srcPosition += normal * normalOffset + direction * centerOffset;
            destPosition += normal * normalOffset - direction * centerOffset;

            var upperTipHalfPosition = destPosition + (normal - direction) * SKRenderingConstans.ArrowTipSize;
            var lowerTipHalfPosition = destPosition - (normal + direction) * SKRenderingConstans.ArrowTipSize;

            srcPosition *= target.PixelsPerUnit;
            destPosition *= target.PixelsPerUnit;
            upperTipHalfPosition *= target.PixelsPerUnit;
            lowerTipHalfPosition *= target.PixelsPerUnit;

            var color = edge.Color ?? _options.StrokeColor;
            paint.Color = SKColor.Parse(color);

            canvas.DrawLine(new(srcPosition.X, srcPosition.Y), new(destPosition.X, destPosition.Y), paint);
            
            if (info.Directed)
            {
                canvas.DrawLine(new(destPosition.X, destPosition.Y), new(upperTipHalfPosition.X, upperTipHalfPosition.Y), paint);
                canvas.DrawLine(new(destPosition.X, destPosition.Y), new(lowerTipHalfPosition.X, lowerTipHalfPosition.Y), paint);
            }
        }
    }
}

