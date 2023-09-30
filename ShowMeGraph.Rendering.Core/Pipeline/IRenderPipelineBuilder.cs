using ShowMeGraph.Contracts;

namespace ShowMeGraph.Rendering.Pipeline;

public interface IRenderPipelineBuilder<TRenderTarget, TRenderable>
{
    public IRenderPipelineBuilder<TRenderTarget, TRenderable> AddRenderer(IRender<TRenderTarget, TRenderable> renderer, uint priority = 0);
    public IRenderPipelineBuilder<TRenderTarget, TRenderable> AddRenderer(Action<TRenderTarget, TRenderable> render, uint priority = 0);
    public IRender<TRenderTarget, TRenderable> Build();
}
