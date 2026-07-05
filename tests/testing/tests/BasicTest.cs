using System.Threading.Tasks;
using Godot;
using Qengu.GodotUtils.Tests;

using static Qengu.GodotUtils.Tests.Assertions;

public class BasicTest
{

    [Setup(InjectionType.ParentNode)]
    public async Task Setup(Node parent)
    {
        await parent.ToSignal(parent.GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        GD.Print("Setup");
    }

    [Test]
    public void FailTest()
    {
        Assert(1 == 2);
    }

    [Test]
    public void SucceedTest()
    {
        Assert(1 == 1);
    }

    [Test(InjectionType.ParentNode)]
    public async Task WaitTest(Node parent)
    {
        await parent.ToSignal(parent.GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
    }

    [Teardown]
    public void Teardown()
    {
        GD.Print("Teardown");
    }

}
