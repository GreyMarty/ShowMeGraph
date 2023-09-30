namespace ShowMeGraph.Animation;

public class CombinedAnimationStep : AnimationStep
{
    public CombinedAnimationStep(IEnumerable<IAnimationStep> steps, TimeSpan duration = default) : base(
        () =>
        {
            foreach (var step in steps)
            {
                step.Execute();
            }

            return null;
        },
        _ =>
        {
            foreach (var step in steps.Reverse())
            {
                step.Undo();
            }
        },
        duration
    ) { }
}
