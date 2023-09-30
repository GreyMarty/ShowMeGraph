namespace ShowMeGraph.Contracts;

public interface IRender<in TRenderTarget, in TRenderable>
{
    public void Render(TRenderTarget target, TRenderable renderable);
}
