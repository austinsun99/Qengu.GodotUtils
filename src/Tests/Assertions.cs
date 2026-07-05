using System.Runtime.CompilerServices;

namespace Qengu.GodotUtils.Tests;

public static class Assertions
{
    public static void Assert(bool predicate, [CallerArgumentExpression(nameof(predicate))] string exp = "")
    {
        if (!predicate) throw new AssertException(string.Format("assertion {0} failed", exp));
    }
}
