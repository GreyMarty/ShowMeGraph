namespace ShowMeGraph.Animation;

public interface IAnimationStep
{
    public TimeSpan Duration { get; }

    public void Execute();
    public void Undo();
}
