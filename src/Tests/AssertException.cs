using System;

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// An exception that can occur during tests when using assertions
/// </summary>
public sealed class AssertException : Exception
{
    public AssertException() : base() { }
    public AssertException(string message) : base(message) { }
}
