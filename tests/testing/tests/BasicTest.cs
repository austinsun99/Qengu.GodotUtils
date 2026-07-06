using System.Threading.Tasks;
using Godot;
using Qengu.GodotUtils.Tests;

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
        Assert.That(1 == 2);
    }

    [Test]
    public void SucceedTest()
    {
        Assert.That(1 == 1);
    }

    [Test]
    public void TestNotNull()
    {
        int x = 5;
        Assert.That(x, Is.NotNull);
    }

    [Test(InjectionType.ParentNode)]
    public async Task SpawnMesh(Node parent)
    {
        MeshInstance3D mesh = new MeshInstance3D()
        {
            Mesh = new BoxMesh(),
            Position = Vector3.Zero
        };
        parent.AddChild(mesh);
        await parent.ToSignal(parent.GetTree().CreateTimer(5f), SceneTreeTimer.SignalName.Timeout);
        mesh.QueueFree();
    }

    [Test]
    public void TestNotNullFail()
    {
        object x = null;
        Assert.That(x, Is.NotNull);
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
