using ShowMeGraph.Contracts;

namespace ShowMeGraph.Rendering.Pipeline;

internal class InlineRenderer<TRenderTarget, TRendererable> : IRender<TRenderTarget, TRendererable>
{
    private readonly Action<TRenderTarget, TRendererable> _render;

    public InlineRenderer(Action<TRenderTarget, TRendererable> render)
    {
        _render = render;
    }

    public void Render(TRenderTarget target, TRendererable renderable)
    {
        _render.Invoke(target, renderable);
    }
}
