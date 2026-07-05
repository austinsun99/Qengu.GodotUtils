using System;
using System.Runtime.CompilerServices;

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// Class of assertions that can be used to assert certain facts
/// </summary>
public static class Assert
{

    /// <summary>
    /// Assert the predicate is true
    /// </summary>
    public static void That(bool predicate, [CallerArgumentExpression(nameof(predicate))] string exp = "")
    {
        if (!predicate) throw new AssertException(string.Format("assertion {0} failed", exp));
    }

    public static void That(object @object, Func<object, bool> predicate,
            [CallerArgumentExpression(nameof(@object))] string objectExp = "",
            [CallerArgumentExpression(nameof(predicate))] string predicateExp = "")
    {
        if (!predicate(@object))
        {
            throw new AssertException(string.Format("{0} fails on {1}", predicateExp, objectExp));
        }
    }

}
