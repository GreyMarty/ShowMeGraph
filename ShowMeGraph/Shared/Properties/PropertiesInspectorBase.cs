using Microsoft.AspNetCore.Components;
using ShowMeGraph.Events;

namespace ShowMeGraph.Shared.Properties;

public abstract class PropertiesInspectorBase : ComponentBase
{
    [Parameter] public EventCallback<PropertyValueChangedEventArgs> OnPropertyChanged { get; set; }
}
