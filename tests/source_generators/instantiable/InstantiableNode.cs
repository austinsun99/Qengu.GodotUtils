using Godot;
using Qengu.GodotUtils.GeneratorAnnotations;

[Instantiable]
public partial class InstantiableNode : Node
{
    public void Init(string initMessage)
    {
        GD.Print($"{nameof(InstantiableNode)} instaniated");
        GD.Print(initMessage);
    }
}
