using System;

namespace Qengu.GodotUtils.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TeardownAttribute(InjectionType injectionType = InjectionType.None) : Attribute
{
    public InjectionType InjectionType { get; } = injectionType;
}
