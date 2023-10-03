namespace ShowMeGraph.Events;

public class PropertyValueChangedEventArgs : EventArgs
{
    public string? PropertyName { get; set; }
    public object? Value { get; set; }

    public PropertyValueChangedEventArgs(string? propertyName = null, object? value = null)
    {
        PropertyName = propertyName;
        Value = value;
    }
}

public delegate void PropertyValueChangedEventHandler(object sender, PropertyValueChangedEventArgs args);
