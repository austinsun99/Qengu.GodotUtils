using System;

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// This attribute specifies the method that after before each test. There should only be one per class. This will run irregardless of whether an exception is thrown during the test.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TeardownAttribute(InjectionType injectionType = InjectionType.None) : Attribute
{
    public InjectionType InjectionType { get; } = injectionType;
}
