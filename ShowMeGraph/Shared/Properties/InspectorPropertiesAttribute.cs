namespace ShowMeGraph.Shared.Properties;

public class InspectorPropertiesAttribute : Attribute
{
    public Type Type { get; set; }

    public InspectorPropertiesAttribute(Type type)
    {
        Type = type;
    }
}
