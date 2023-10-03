using ShowMeGraph.Animation;
using ShowMeGraph.Pages;

namespace ShowMeGraph.Managers;

public class AnimationManager
{
    private readonly IndexViewModel _index;

    public AnimationManager(IndexViewModel index)
    {
        _index = index;
    }

    public void Play(IAnimation animation)
    {
        _index.Animation?.Reset();
        _index.Animation = animation;
    }

    public void Stop()
    {
        _index.Animation?.Reset();
        _index.Animation = null;
    }
}
