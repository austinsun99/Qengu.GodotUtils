using Godot;
using System;

public partial class Instantiator : Node
{
    public override void _Ready()
    {
        var node = InstantiableNode.Instantiate("Init Message Test");
        AddChild(node);
    }
}
