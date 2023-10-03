using ShowMeGraph.Contracts;
using ShowMeGraph.Data;

namespace ShowMeGraph.Shared.Properties;

public class GraphPropertiesInspectorViewModel : ViewModelBase
{
    private readonly VisGraph _model;

    public int NodesCount => _model.Value.VertexCount;
    public int EdgesCount => _model.Value.EdgeCount;

    public bool IsDirected
    {
        get => _model.IsDirected;
        set
        {
            _model.SetDirected(value);
            OnPropertyChanged(nameof(IsDirected), _model.IsDirected);
        }
    }

    public GraphPropertiesInspectorViewModel(VisGraph model)
    {
        _model = model;
    }
}
