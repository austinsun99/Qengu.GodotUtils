using System;
using Godot;

namespace Qengu.GodotUtils.Raycast;

public sealed record RaycastResult3D
{
    public required Vector3 Position { get; init; }
    public required Vector3 Normal { get; init; }
    public required GodotObject Collider { get; init; }
    public required Rid RID { get; init; }
    public required int Shape { get; init; }

    private RaycastResult3D() { }

    public static RaycastResult3D From(Godot.Collections.Dictionary result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return new RaycastResult3D()
        {
            Position = (Vector3)result["position"],
            Normal = (Vector3)result["normal"],
            Collider = (GodotObject)result["collider"],
            RID = (Rid)result["rid"],
            Shape = (int)result["shape"],
        };
    }
}
