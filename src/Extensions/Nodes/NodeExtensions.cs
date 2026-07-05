using Godot;

namespace Qengu.GodotUtils.Extensions;

/// <summary>
/// Extension methods for nodes
/// </summary>
public static class NodeExtensions
{

    /// <summary>
    /// Returns the first child of the type T, or null if there aren't any
    /// </summary>
    /// <typeparam name="T">The type to find</typeparam>
    /// <param name="includeInternal"> Whether or not to include internal nodes </param>
    /// <returns>The child, or null if there are none</returns>
    public static T? GetChildOfTypeOrNull<T>(this Node node, bool includeInternal = false) where T : Node
    {
        foreach (var child in node.GetChildren(includeInternal))
            if (child is T childAsT) return childAsT;
        return null;
    }

    /// <summary>
    /// Queue frees all children of the node. Does not free the node
    /// </summary>
    /// <param name="includeInternal"> Whether or not to include internal nodes </param>
    public static void QueueFreeChildren(this Node node, bool includeInternal = false)
    {
        foreach (var child in node.GetChildren(includeInternal)) child.QueueFree();
    }

}
