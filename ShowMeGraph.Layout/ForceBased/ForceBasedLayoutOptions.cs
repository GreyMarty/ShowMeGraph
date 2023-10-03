namespace ShowMeGraph.Layout.ForceBased;

public class ForceBasedLayoutOptions
{
    public float Stiffness { get; set; }
    public float EdgeLength { get; set; }
    public float PassiveStiffness { get; set; }
    public float PassiveDistance { get; set; }
    public float Repulsion { get; set; }
    public float ForceModifier { get; set; }
    public float MaxOffset { get; set; }
    public float UpdateStep { get; set; }

    public static ForceBasedLayoutOptions Default => new()
    {
        Stiffness = 2,
        EdgeLength = 2,
        PassiveStiffness = 0.2f,
        PassiveDistance = 5,
        Repulsion = 1,
        ForceModifier = 0.5f,
        MaxOffset = 10,
        UpdateStep = 0.1f,
    };
}
