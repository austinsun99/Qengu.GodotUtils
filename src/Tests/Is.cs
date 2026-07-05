namespace Qengu.GodotUtils.Tests;

/// <summary>
/// Class of methods that take in an object, and return a bool based on some predicate
/// </summary>
public static class Is
{
    public static bool Null(object @object)
    {
        return @object is null;
    }

    public static bool NotNull(object @object)
    {
        return !Null(@object);
    }
}
