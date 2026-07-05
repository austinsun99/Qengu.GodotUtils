using System;

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// This attribute specifies the method that runs before each test. There should only be one per class.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SetupAttribute(InjectionType injectionType = InjectionType.None) : Attribute
{
    public InjectionType InjectionType { get; } = injectionType;
}
