﻿using QuikGraph;
using ShowMeGraph.Animation;
using ShowMeGraph.Contracts;
using ShowMeGraph.Data;
using ShowMeGraph.Pages;
using System.Xml.Linq;

namespace ShowMeGraph.Algorithms;

public class DijkstraAnimatedAlgorithm : IAnimatedAlgorithm
{
    private const string ProcessingColor = "#ffaaaa";
    private const string ProcessedColor = "#aaffaa";
    private const string NotVisitedColor = "#aaaaaa";

    private readonly IndexViewModel _index;

    public string Name => "Dijkstra";

    public DijkstraAnimatedAlgorithm(IndexViewModel index)
    {
        _index = index;
    }

    public void Animate()
    {
        var steps = new List<IAnimationStep>();

        var graph = _index.Graph.Value;
        var startNode = _index.SelectionManager.SelectedObjects.FirstOrDefault() as VisNode ?? graph.Vertices.FirstOrDefault();

        if (startNode is null)
        {
            return;
        }

        var distances = new Dictionary<VisNode, float>();
        var visited = new HashSet<VisNode>();

        var queue = new PriorityQueue<VisNode, float>();

        foreach (var node in graph.Vertices)
        {
            distances[node] = float.PositiveInfinity;
        }

        distances[startNode] = 0;
        queue.Enqueue(startNode, 0);

        steps.AddRange(Setup(graph));

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            var distance = distances[node];

            visited.Add(node);

            steps.Add(Step(node, float.IsInfinity(distance) ? "inf" : distance.ToString(), ProcessedColor));

            if ((graph as AdjacencyGraph<VisNode, VisEdge>)?.TryGetOutEdges(node, out var edges) != true)
            {
                edges = (graph as UndirectedGraph<VisNode, VisEdge>)?.AdjacentEdges(node);
            }

            foreach (var edge in edges)
            {
                var dest = edge.Source == node
                    ? edge.Target
                    : edge.Source;

                if (visited.Contains(dest))
                {
                    continue;
                }

                var newDistance = distance + edge.Weight;

                steps.Add(Step(dest, float.IsInfinity(distances[dest]) ? "inf" : distances[dest].ToString(), ProcessingColor));

                if (newDistance < distances[dest])
                {
                    distances[dest] = newDistance;
                    queue.Enqueue(dest, newDistance);
                }
            }
        }

        var animation = new StepByStepAnimation(steps.ToArray());
        _index.AnimationManager.Play(animation);
    }

    private IEnumerable<IAnimationStep> Setup(IMutableVertexAndEdgeSet<VisNode, VisEdge> graph)
    {
        return graph.Vertices.Select(n => new CombinedAnimationStep(new[] 
        {
            new SetterAnimationStep<string>(() => n.Text, v => n.Text = v, "inf"),
            new SetterAnimationStep<string>(() => n.Color, v => n.Color = v, NotVisitedColor)
        }, TimeSpan.FromMilliseconds(33)));
    }

    private IAnimationStep Step(VisNode node, string? text, string? color)
    {
        return new CombinedAnimationStep(new[]
        {
            new SetterAnimationStep<string>(() => node.Text, v => node.Text = v, text),
            new SetterAnimationStep<string>(() => node.Color, v => node.Color = v, color)
        }, TimeSpan.FromMilliseconds(100));
    }
}
