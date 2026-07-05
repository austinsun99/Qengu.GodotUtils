using System;
using System.Reflection;

namespace Qengu.GodotUtils.Tests;

public sealed record TestMethodInfo(MethodInfo MethodInfo, InjectionType InjectionType);
