using ShowMeGraph.Contracts;

namespace ShowMeGraph.Rendering.Pipeline;

public sealed partial class RenderPipeline<TRenderTarget, TRenderable> : IRender<TRenderTarget, TRenderable>
{
    private readonly IRender<TRenderTarget, TRenderable>[] _renderers;

    private RenderPipeline(IEnumerable<IRender<TRenderTarget, TRenderable>> renderers)
    {
        _renderers = renderers.ToArray();
    }

    public void Render(TRenderTarget target, TRenderable renderable)
    {
        foreach (var renderer in _renderers)
        {
            renderer.Render(target, renderable);
        }
    }

    public static IRenderPipelineBuilder<TRenderTarget, TRenderable> CreateBuilder() => new Builder();
}
