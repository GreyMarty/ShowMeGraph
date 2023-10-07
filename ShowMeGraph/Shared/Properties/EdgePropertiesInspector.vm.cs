using ShowMeGraph.Contracts;
using ShowMeGraph.Data;

namespace ShowMeGraph.Shared.Properties;

public class EdgePropertiesInspectorViewModel : ViewModelBase
{
    private readonly UiEdge _model;

    public int Weight
    {
        get => _model.Weight;
        set
        {
            _model.Weight = value;
            OnPropertyChanged(nameof(Weight), Weight);
        }
    }

    public EdgePropertiesInspectorViewModel(UiEdge model)
    {
        _model = model;
    }
}
