using Microsoft.AspNetCore.Components;
using ShowMeGraph.Events;

namespace ShowMeGraph.Contracts;

public interface INotifyPropertyValueChanged
{
    public event PropertyValueChangedEventHandler? PropertyChanged;
}