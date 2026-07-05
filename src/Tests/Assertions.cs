using System.Runtime.CompilerServices;

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// Class of assertions that can be used to assert certain facts
/// </summary>
public static class Assertions
{
    /// <summary>
    /// Assert the predicate is true
    /// </summary>
    public static void Assert(bool predicate, [CallerArgumentExpression(nameof(predicate))] string exp = "")
    {
        if (!predicate) throw new AssertException(string.Format("assertion {0} failed", exp));
    }
}
