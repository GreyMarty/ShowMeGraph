namespace ShowMeGraph.Animation;

public class StepByStepAnimation : IAnimation
{
    private readonly IAnimationStep[] _steps;

    private int _stepIndex;
    private TimeSpan _stepElapsedTime;

    public bool Finished { get; private set; }
    
    public TimeSpan ElapsedTime { get; private set; }
    public TimeSpan TotalTime { get; private set; }


    public StepByStepAnimation(IAnimationStep[] steps)
    {
        _steps = steps;

        TotalTime = _steps.Aggregate(TimeSpan.Zero, (acc, x) => acc + x.Duration);
    }

    public void Proceed(TimeSpan duration)
    {
        if (_stepIndex >= _steps.Length)
        {
            Finished = true;
            return;
        }

        _stepElapsedTime += duration;
        ElapsedTime = ElapsedTime + duration < TotalTime ? ElapsedTime + duration : TotalTime;

        while (_stepIndex < _steps.Length && _stepElapsedTime > _steps[_stepIndex].Duration)
        {
            _steps[_stepIndex].Execute();
            _stepElapsedTime -= _steps[_stepIndex].Duration;
            _stepIndex++;
        }

        Finished = _stepIndex >= _steps.Length;
    }

    public void MoveTo(TimeSpan duration)
    {
        if (duration < TimeSpan.Zero)
        {
            duration = TimeSpan.Zero;
        }

        if (duration > TotalTime)
        {
            duration = TotalTime;
        }

        while (_stepIndex < _steps.Length && duration - ElapsedTime > _steps[_stepIndex].Duration * 0.75f)
        {
            Next();
        }

        while (_stepIndex > 0 && ElapsedTime - duration > _steps[_stepIndex-1].Duration * 0.75f)
        {
            Previous();
        }
    }

    public void Next()
    {
        if (_stepIndex >= _steps.Length)
        {
            return;
        }

        _stepElapsedTime = TimeSpan.Zero;
        ElapsedTime += _steps[_stepIndex].Duration;

        _steps[_stepIndex].Execute();
        _stepIndex++;

        Finished = _stepIndex >= _steps.Length;
    }

    public void Previous()
    {
        if (_stepIndex == 0)
        {
            return;
        }

        _stepElapsedTime = TimeSpan.Zero;
        
        _stepIndex--;
        _steps[_stepIndex].Undo();

        ElapsedTime -= _steps[_stepIndex].Duration;

        Finished = false;
    }

    public void Reset()
    {
        _stepElapsedTime = TimeSpan.Zero;
        ElapsedTime = TimeSpan.Zero;
        Finished = false;

        while (_stepIndex > 0)
        {
            _stepIndex--;
            _steps[_stepIndex].Undo();
        }
    }
}
