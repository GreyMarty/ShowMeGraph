namespace ShowMeGraph.Animation;

public interface IAnimation
{
    public bool Finished { get; }

    public TimeSpan ElapsedTime { get; }
    public TimeSpan TotalTime { get; }

    public void Proceed(TimeSpan duration);

    public void MoveTo(TimeSpan duration);

    public void Next();

    public void Previous();

    public void Reset();
}
