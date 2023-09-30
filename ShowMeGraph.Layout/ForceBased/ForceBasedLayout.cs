using ShowMeGraph.Contracts;
using ShowMeGraph.Data;

namespace ShowMeGraph.Layout.ForceBased;

public class ForceBasedLayout : ILayout
{
    private const float Eps = 0.01f;

    private readonly Random _random;

    public ForceBasedLayoutOptions Options { get; set; }

    public ForceBasedLayout(ForceBasedLayoutOptions? options = null)
    {
        _random = new();
        Options = options ?? ForceBasedLayoutOptions.Default;
    }

    public void SetUp(IGraphLayoutInfo info)
    {
        var nodesCount = info.Nodes.Count();

        foreach (var node in info.Nodes)
        {
            var estimatedRadius = MathF.Sqrt(Options.EdgeLength * nodesCount);

            var angle = _random.NextSingle() * MathF.PI * 2;
            var radius = _random.NextSingle() * estimatedRadius;

            node.Position = new(MathF.Cos(angle) * estimatedRadius, MathF.Sin(angle) * radius);
        }
    }

    public void Arrange(IGraphLayoutInfo info)
    {
        foreach (var node in info.Nodes)
        {
            if (node.Fixed)
            {
                continue;
            }

            var force = new Vector2F();

            foreach (var anotherNode in info.Nodes)
            {
                if (info.AreAdjacent(node, anotherNode) || node == anotherNode)
                {
                    continue;
                }

                force += CalculateRepulsion(node.Position, anotherNode.Position);
                force += CalculatePassiveAttraction(node.Position, anotherNode.Position);
            }

            foreach (var anotherNode in info.AdjacentNodes(node))
            {
                if (node == anotherNode)
                {
                    continue;
                }

                force += CalculateAttraction(node.Position, anotherNode.Position);
            }

            node.Position += force * (Options.ForceModifier * Options.UpdateStep);
        }
    }

    private Vector2F CalculateAttraction(Vector2F a, Vector2F b)
    {
        EnsureNotTheSamePoint(ref a, ref b);

        var direction = b - a;
        var distance = direction.Magnitude;

        return direction.Normalized() * Options.Stiffness * MathF.Log(distance / Options.EdgeLength);
    }

    private Vector2F CalculatePassiveAttraction(Vector2F a, Vector2F b)
    {
        EnsureNotTheSamePoint(ref a, ref b);

        var direction = b - a;
        var distance = direction.Magnitude;

        return direction.Normalized() * Options.PassiveStiffness * MathF.Log(distance / Options.PassiveDistance);
    }

    private Vector2F CalculateRepulsion(Vector2F a, Vector2F b)
    {
        EnsureNotTheSamePoint(ref a, ref b);

        var direction = a - b;
        var distance = direction.Magnitude;

        return direction.Normalized() * Options.Repulsion / MathF.Pow(distance, 2);
    }

    private void EnsureNotTheSamePoint(ref Vector2F a, ref Vector2F b)
    {
        if (a.X == b.X && a.Y == b.Y)
        {
            a.X += _random.NextSingle() * Eps;
            a.Y += _random.NextSingle() * Eps;
        }
    }
}
