namespace ShowMeGraph.Animation;

public class AnimationStep : IAnimationStep
{
    private readonly Func<object?>? _execute;
    private readonly Action<object>? _undo;

    private object? _state;

    public TimeSpan Duration { get; }

    public AnimationStep(TimeSpan duratuion = default)
    {
        Duration = duratuion;
    }

    public AnimationStep(Func<object?>? execute, Action<object>? undo, TimeSpan duration = default) : this(duration)
    {
        _execute = execute;
        _undo = undo;
    }

    public void Execute()
    {
        _state = _execute?.Invoke();
    }

    public void Undo()
    {
        _undo?.Invoke(_state);
    }
}
