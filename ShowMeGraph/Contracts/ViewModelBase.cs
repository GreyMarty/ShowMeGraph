using ShowMeGraph.Events;

namespace ShowMeGraph.Contracts;

public abstract class ViewModelBase : INotifyPropertyValueChanged
{
    public event PropertyValueChangedEventHandler? PropertyChanged;

    protected void SetField<T>(ref T field, T value, string? propertyName = null)
    {
        field = value;
        OnPropertyChanged(propertyName, value);
    }

    protected void OnPropertyChanged(string? propertyName, object? value)
    {
        PropertyChanged?.Invoke(this, new PropertyValueChangedEventArgs(propertyName, value));
    }
}
