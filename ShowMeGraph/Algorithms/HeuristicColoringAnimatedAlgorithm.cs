using ShowMeGraph.Animation;
using ShowMeGraph.Contracts;
using ShowMeGraph.Data;
using ShowMeGraph.Helpers;
using ShowMeGraph.Pages;

namespace ShowMeGraph.Algorithms;

public class HeuristicColoringAnimatedAlgorithm : IAnimatedAlgorithm
{
    private readonly IndexViewModel _index;

    public string Name => "Heuristic Coloring";
    
    public HeuristicColoringAnimatedAlgorithm(IndexViewModel index)
    {
        _index = index;
    }

    public void Animate()
    {
        var steps = new List<IAnimationStep>();

        var paletteGenerator = new GoldenRatioColorPalette(0.4);
        var palette = new List<string>();

        var graph = _index.Graph;
        var nodes = graph.Vertices.OrderBy(x => -graph.OutEdges(x).Count());

        var colorsCount = 0;
        var colors = new Dictionary<UiVertex, int>();

        steps.AddRange(Setup(graph));
        
        foreach (var node in nodes)
        {
            var adjacentColors = graph.OutEdges(node)
                .Select(x => x.Source == node ? x.Target : x.Source)
                .Where(colors.ContainsKey)
                .Select(x => colors[x])
                .ToHashSet();

            var colored = false;

            for (int i = 0; i < colorsCount; i++)
            {
                while (palette.Count < i)
                {
                    palette.Add(paletteGenerator.Next());
                }

                steps.Add(Step(node, palette[i]));

                if (!adjacentColors.Contains(i))
                {
                    colors[node] = i;
                    colored = true;
                    break;
                }
            }

            if (colored)
            {
                continue;
            }

            colors[node] = colorsCount++;

            while (palette.Count < colorsCount)
            {
                palette.Add(paletteGenerator.Next());
            }

            steps.Add(Step(node, palette[colorsCount - 1]));
        }

        var animation = new StepByStepAnimation(steps.ToArray());
        _index.AnimationManager.Play(animation);
    }

    private IEnumerable<IAnimationStep> Setup(IUiGraph<UiVertex, UiEdge> graph)
    {
        return graph.Edges.Select(x => new SetterAnimationStep<string>(() => x.DisplayedText, v => x.Text = v, "", TimeSpan.FromMilliseconds(33)));
    }

    private IAnimationStep Step(UiVertex node, string? color)
    {
        return new SetterAnimationStep<string>(() => node.Color, v => node.Color = v, color, TimeSpan.FromMilliseconds(100));
    }
}
