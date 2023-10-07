using ShowMeGraph.Contracts;
using ShowMeGraph.Data;

namespace ShowMeGraph.Shared.Properties;

public class GraphPropertiesInspectorViewModel : ViewModelBase
{
    private readonly UiGraphUnion<DirectedUiGraph, UndirectedUiGraph> _model;

    public int VertexCount => _model.VertexCount;
    public int EdgesCount => _model.EdgeCount;

    public bool IsDirected
    {
        get => _model.IsDirected;
        set
        {
            _model.IsDirected = value;
            OnPropertyChanged(nameof(IsDirected), _model.IsDirected);
        }
    }

    public GraphPropertiesInspectorViewModel(UiGraphUnion<DirectedUiGraph, UndirectedUiGraph> model)
    {
        _model = model;
    }
}
