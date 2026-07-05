using System;

namespace Qengu.GodotUtils.Tests;

public enum InjectionType
{
    None,
    ParentNode,
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TestAttribute(InjectionType injectionType = InjectionType.None) : Attribute
{
    public InjectionType InjectionType { get; } = injectionType;
}
