using ShowMeGraph.Data;
using ShowMeGraph.Pages;
using ShowMeGraph.Shared;

namespace ShowMeGraph.Tools;

public class RandomizeTool : ITool
{
    private const float EdgeProbability = 0.05f;

    private readonly IndexViewModel _index;

    public string Icon => CustomIcons.Outlined.Dice;
    public bool Selectable => false;

    public RandomizeTool(IndexViewModel index)
    {
        _index = index;
    }

    public void Activate()
    {
        _index.AnimationManager.Stop();

        var random = new Random();

        _index.Graph.Value.RemoveEdgeIf(_ => true);

        foreach (var src in _index.Graph.Value.Vertices)
        {
            foreach (var dest in _index.Graph.Value.Vertices)
            {
                if (src == dest || random.NextSingle() > EdgeProbability)
                {
                    continue;
                }

                var edge = new VisEdge(src, dest)
                {
                    Weight = random.Next(0, 100)
                };
                _index.Graph.Value.AddEdge(edge);
            }
        }
    }

    public void Deactivate()
    {
    }
}
