﻿using ShowMeGraph.Contracts;
using SkiaSharp;

namespace ShowMeGraph.Rendering.SkiaSharp;

public class SKNodeRenderer : IRender<SKRenderTarget, IGraphRenderInfo>
{
    private readonly SKRenderingOptions _options;

    public SKNodeRenderer(SKRenderingOptions options)
    {
        _options = options;
    }

    public void Render(SKRenderTarget target, IGraphRenderInfo info)
    {
        var canvas = target.Canvas;

        var fillPaint = new SKPaint
        {
            Color = SKColor.Parse(_options.NodeFillColor),
        };

        var strokePaint = new SKPaint
        {
            IsAntialias = true,
            Color = SKColor.Parse(_options.StrokeColor),
            StrokeWidth = _options.StrokeThickness,
            IsStroke = true
        };

        foreach (var node in info.Vertices)
        {
            var position = node.Position * target.PixelsPerUnit;

            var color = node.Color ?? _options.NodeFillColor;
            fillPaint.Color = SKColor.Parse(color);
            
            var strokeColor = node.DisplayedStrokeColor ?? _options.StrokeColor;
            strokePaint.Color = SKColor.Parse(strokeColor);
            strokePaint.StrokeWidth = node.Fixed 
                ? _options.StrokeThickness * 1.5f
                : _options.StrokeThickness;

            canvas.DrawCircle(position.X, position.Y, SKRenderingConstans.NodeRadius * target.PixelsPerUnit, fillPaint);
            canvas.DrawCircle(position.X, position.Y, SKRenderingConstans.NodeRadius * target.PixelsPerUnit, strokePaint);
        }
    }
}

