using System.Threading.Tasks;
using Godot;
using Qengu.GodotUtils.Tests;

using static Qengu.GodotUtils.Tests.Assertions;

public class BasicTest
{
    [Test]
    public void Sum()
    {
        Assert(1 == 2);
    }

    [NodeTest]
    public async Task WaitTest(Node parent)
    {
        await parent.ToSignal(parent.GetTree().CreateTimer(5f), SceneTreeTimer.SignalName.Timeout);
    }

}
