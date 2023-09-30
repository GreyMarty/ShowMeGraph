using ShowMeGraph.Contracts;
using ShowMeGraph.Rendering.Pipeline;

namespace ShowMeGraph.Rendering.SkiaSharp;

public static class SKRenderPipeline
{
    public static IRenderPipelineBuilder<SKRenderTarget, IGraphRenderInfo> CreateDefaultBuilder(SKRenderingOptions? options = default)
    {
        options ??= SKRenderingOptions.Default;

        return RenderPipeline<SKRenderTarget, IGraphRenderInfo>.CreateBuilder()
            .AddRenderer(new SKRendererSetup(options))
            .AddRenderer(new SKEdgeRenderer(options))
            .AddRenderer(new SKEdgeTextRenderer(options))
            .AddRenderer(new SKNodeRenderer(options))
            .AddRenderer(new SKNodeTextRenderer(options));
    }
}
