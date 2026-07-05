using System.Threading.Tasks;
using Godot;

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// Various helper methods for managing nodes during tests. Most methods will wait a frame during an action.
/// </summary>
public static class NodeTestHelper
{
    public static async Task<(bool err, Node? instance)> InstantiateAndAddScene<T>(Node parent, string path) where T : Node
    {
        (bool err, T? obj) = GodotUtils.InstantiateScene<T>(path);
        if (err) return (err, null);

        await AwaitProcessFrame(parent.GetTree());
        parent.AddChild(obj);
        return (false, obj);
    }

    public static async Task FreeScene(SceneTree tree, Node instance)
    {
        instance.QueueFree();
        await AwaitProcessFrame(tree);
    }

    public static async Task AwaitProcessFrame(SceneTree tree)
    {
        await tree.ToSignal(tree, SceneTree.SignalName.ProcessFrame);
    }

}
