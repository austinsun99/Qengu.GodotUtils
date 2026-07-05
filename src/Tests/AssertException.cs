using System;

namespace Qengu.GodotUtils.Tests;

public sealed class AssertException : Exception
{
    public AssertException() : base() { }
    public AssertException(string message) : base(message) { }
}
