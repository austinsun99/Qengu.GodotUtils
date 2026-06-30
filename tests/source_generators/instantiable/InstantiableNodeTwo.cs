using Qengu.GodotUtils.GeneratorAnnotations;
using Godot;

[Instantiable(nameof(CustomInitName), "instantiable_node_two_different_name")]
public partial class InstantiableNodeTwo : Node
{

    public void CustomInitName(int x = 3)
    {
        GD.Print(x);
    }

}
