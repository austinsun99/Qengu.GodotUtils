using System;

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// An attribute marking a method as a test method, which will automatically be called by a test runner.
/// An injection type can be specified in order to get some dependency injection
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TestAttribute(InjectionType injectionType = InjectionType.None) : Attribute
{
    public InjectionType InjectionType { get; } = injectionType;
}
