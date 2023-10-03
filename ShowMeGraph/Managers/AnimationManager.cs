using ShowMeGraph.Animation;
using ShowMeGraph.Pages;

namespace ShowMeGraph.Managers;

public class AnimationManager
{
    private readonly IndexViewModel _index;

    public IAnimation? Animation { get; private set; }

    public AnimationManager(IndexViewModel index)
    {
        _index = index;
    }

    public void Play(IAnimation animation)
    {
        Animation?.Reset();
        Animation = animation;
        _index.OnStateHasChanged();
    }

    public void Stop()
    {
        Animation?.Reset();
        Animation = null;
        _index?.OnStateHasChanged();
    }
}
