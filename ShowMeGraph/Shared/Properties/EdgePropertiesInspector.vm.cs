using ShowMeGraph.Contracts;
using ShowMeGraph.Data;

namespace ShowMeGraph.Shared.Properties;

public class EdgePropertiesInspectorViewModel : ViewModelBase
{
    private readonly VisEdge _model;

    public int Weight
    {
        get => _model.Weight;
        set
        {
            _model.Weight = value;
            OnPropertyChanged(nameof(Weight), Weight);
        }
    }

    public EdgePropertiesInspectorViewModel(VisEdge model)
    {
        _model = model;
    }
}
