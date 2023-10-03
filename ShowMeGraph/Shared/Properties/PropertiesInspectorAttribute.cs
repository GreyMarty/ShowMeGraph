namespace ShowMeGraph.Shared.Properties;

public class PropertiesInspectorAttribute : Attribute
{
    public Type Type { get; set; }

    public PropertiesInspectorAttribute(Type type)
    {
        Type = type;
    }
}
