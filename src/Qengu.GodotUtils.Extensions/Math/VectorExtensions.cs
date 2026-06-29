using Godot;

namespace Qengu.GodotUtils.Extensions;

/// <summary>
/// Extension methods for Vector2
/// </summary>
public static class VectorExtensions
{

    /// <summary>
    /// Creates a Vector3 from a Vector2 by adding a X component 
    /// </summary>
    /// <param name="Vector2">The vector that is to be appended</param>
    /// <param name="@value">The x value</param>
    /// <returns>The vector with the extra component</returns>
    public static Vector3 WithX(this Vector2 vec, float @value) => new(@value, vec.X, vec.Y);

    /// <summary>
    /// Creates a Vector3 from a Vector2 by adding a Y component 
    /// </summary>
    /// <param name="Vector2">The vector that is to be appended</param>
    /// <param name="@value">The y value</param>
    /// <returns>The vector with the extra component</returns>
    public static Vector3 WithY(this Vector2 vec, float @value) => new(vec.X, @value, vec.Y);

    /// <summary>
    /// Creates a Vector3 from a Vector2 by adding a Z component 
    /// </summary>
    /// <param name="Vector2">The vector that is to be appended</param>
    /// <param name="@value">The z value</param>
    /// <returns>The vector with the extra component</returns>
    public static Vector3 WithZ(this Vector2 vec, float @value) => new(vec.X, vec.Y, @value);

    /// <summary>
    /// Swaps the components of the vector
    /// </summary>
    /// <param name="vec"> The vector to be modified <paramref name="vec"/>
    /// <returns>The vector with the components swapped</returns>
    public static Vector2 Swap(this Vector2 vec) => new(vec.Y, vec.X);

}

