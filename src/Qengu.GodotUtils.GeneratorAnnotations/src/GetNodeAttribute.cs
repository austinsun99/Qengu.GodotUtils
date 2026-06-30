namespace Qengu.GodotUtils.GeneratorAnnotations;

/// <summary>
/// This attribute is applied to a property that is of type Node.
/// The node is automatically fetched based on the name of the property, unless
/// otherwise specified by <paramref name="path"/>
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
sealed public class GetNodeAttribute(string? path) : Attribute
{
    public string? Path { get; } = path;
}
