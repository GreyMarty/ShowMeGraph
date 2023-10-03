using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ShowMeGraph.Shared.Properties;

public static class PropertiesInspectorResolver
{
    private static readonly Dictionary<Type, Type> _inspectors;

    [SuppressMessage("Minor Code Smell", "S3963:\"static\" fields should be initialized inline", Justification = "<Pending>")]
    static PropertiesInspectorResolver()
    {
        var assembly = typeof(PropertiesInspectorResolver).Assembly;

        _inspectors = assembly.GetTypes()
            .Select(t => new { Type = t, Attribute = t.GetCustomAttribute<PropertiesInspectorAttribute>() })
            .Where(x => x.Attribute is not null)
            .ToDictionary(p => p.Attribute!.Type, p => p.Type);
    }

    public static PropertiesInspectorBase? Resolve(Type inspectedObjectType)
    {
        var type = _inspectors.GetValueOrDefault(inspectedObjectType);

        if (type is null)
        {
            return null;
        }

        return Activator.CreateInstance(type) as PropertiesInspectorBase;
    }

    public static Type? ResolveType(Type inspectedObjectType) => _inspectors.GetValueOrDefault(inspectedObjectType);

    public static PropertiesInspectorBase? Resolve<T>() => Resolve(typeof(T));
}
