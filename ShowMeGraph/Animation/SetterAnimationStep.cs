namespace ShowMeGraph.Animation;

public class SetterAnimationStep<T> : AnimationStep
{
    public SetterAnimationStep(Func<T?> getter, Action<T?> setter, T? value, TimeSpan duration = default) : base(
        () => 
        {
            var state = getter();
            setter(value);
            return state;
        },
        state => 
        {
            setter((T)state);
        },
        duration
    ) { }
}
