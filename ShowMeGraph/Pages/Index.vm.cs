using ShowMeGraph.Algorithms;
using ShowMeGraph.Animation;
using ShowMeGraph.Blazor.Events;
using ShowMeGraph.Contracts;
using ShowMeGraph.Data;
using ShowMeGraph.Layout.ForceBased;
using ShowMeGraph.Managers;
using ShowMeGraph.Tools;

namespace ShowMeGraph.Pages;

public class IndexViewModel : ViewModelBase
{
    private UiGraphUnion<DirectedUiGraph, UndirectedUiGraph> _graph;
    private ForceBasedLayout _graphLayout;
    private GraphLayoutInfo _graphLayoutInfo;
    private GraphRenderInfo _graphRenderInfo;
    
    private bool _allowPanning = true;
    private bool _allowDragging = true;

    public UiGraphUnion<DirectedUiGraph, UndirectedUiGraph> Graph
    {
        get => _graph;
        set => SetField(ref _graph, value, nameof(Graph));
    }
    public ForceBasedLayout GraphLayout
    {
        get => _graphLayout;
        set => SetField(ref _graphLayout, value, nameof(GraphLayout));
    }
    public GraphLayoutInfo GraphLayoutInfo
    {
        get => _graphLayoutInfo;
        set => SetField(ref _graphLayoutInfo, value, nameof(GraphLayoutInfo));
    }
    public GraphRenderInfo GraphRenderInfo
    {
        get => _graphRenderInfo;
        set => SetField(ref _graphRenderInfo, value, nameof(GraphRenderInfo));
    }

    public SelectionManager SelectionManager { get; }
    public HoverManager HoverManager { get; }
    public AnimationManager AnimationManager { get; }

    public bool AllowPanning
    {
        get => _allowPanning;
        set => SetField(ref _allowPanning, value, nameof(AllowPanning));
    }
    public bool AllowDragging
    {
        get => _allowDragging;
        set => SetField(ref _allowDragging, value, nameof(AllowDragging));
    }

    public ITool[] Tools { get; }
    public IAnimatedAlgorithm[] Algorithms { get; }

    public event EventHandler? StateHasChanged;
    public event EventHandler<NodeEventArgs>? NodeHoverEnter;
    public event EventHandler<NodeEventArgs>? NodeHoverLeave;
    public event EventHandler<NodeEventArgs>? NodeClicked;
    public event EventHandler<EdgeEventArgs>? EdgeHoverEnter;
    public event EventHandler<EdgeEventArgs>? EdgeHoverLeave;
    public event EventHandler<EdgeEventArgs>? EdgeClicked;
    public event EventHandler<LocalMouseEventArgs>? WhitespaceClicked;

    public IndexViewModel()
    {
        _graph = new(true);
        _graphLayout = new();
        _graphLayoutInfo = new(_graph);
        _graphRenderInfo = new(_graph);

        Tools = new ITool[] 
        { 
            new CursorTool(this),
            new NodeCreationTool(this),
            new EdgeCreationTool(this),
            new EraseTool(this),
            new RandomizeTool(this)
        };

        Algorithms = new IAnimatedAlgorithm[]
        {
            new DijkstraAnimatedAlgorithm(this),
            new HeuristicColoringAnimatedAlgorithm(this)
        };

        SelectionManager = new(this);
        HoverManager = new(this);
        AnimationManager = new(this);
    }

    public void OnNodeHoverEnter(NodeEventArgs e) => NodeHoverEnter?.Invoke(this, e);

    public void OnNodeHoverLeave(NodeEventArgs e) => NodeHoverLeave?.Invoke(this, e);

    public void OnNodeClicked(NodeEventArgs e) => NodeClicked?.Invoke(this, e);

    public void OnEdgeHoverEnter(EdgeEventArgs e) => EdgeHoverEnter?.Invoke(this, e);

    public void OnEdgeHoverLeave(EdgeEventArgs e) => EdgeHoverLeave?.Invoke(this, e);

    public void OnEdgeClicked(EdgeEventArgs e) => EdgeClicked?.Invoke(this, e);

    public void OnWhitespaceClicked(LocalMouseEventArgs e) => WhitespaceClicked?.Invoke(this, e);

    public void OnStateHasChanged() => StateHasChanged?.Invoke(this, EventArgs.Empty);
}
