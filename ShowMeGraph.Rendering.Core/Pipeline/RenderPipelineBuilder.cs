using ShowMeGraph.Contracts;

namespace ShowMeGraph.Rendering.Pipeline;

public sealed partial class RenderPipeline<TRenderTarget, TRenderable> : IRender<TRenderTarget, TRenderable>
{
    private sealed class Builder : IRenderPipelineBuilder<TRenderTarget, TRenderable>
    {
        private readonly SortedList<uint, IRender<TRenderTarget, TRenderable>> _renderers = new();

        public IRenderPipelineBuilder<TRenderTarget, TRenderable> AddRenderer(IRender<TRenderTarget, TRenderable> renderer, uint priority = default)
        {
            if (priority == default)
            {
                priority = (uint)_renderers.Count;
            }

            _renderers.Add(priority, renderer);
            return this;
        }

        public IRenderPipelineBuilder<TRenderTarget, TRenderable> AddRenderer(Action<TRenderTarget, TRenderable> render, uint priority = default) =>
            AddRenderer(new InlineRenderer<TRenderTarget, TRenderable>(render), priority);

        public IRender<TRenderTarget, TRenderable> Build()
        {
            return new RenderPipeline<TRenderTarget, TRenderable>(_renderers.Select(x => x.Value));
        }
    }
}
