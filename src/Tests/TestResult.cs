using Qengu.GodotUtils.Tests;

namespace Qengu.GodotUtils.Tests;

public sealed record TestResult(bool Success, string FileName, string MethodName, string ExceptionMessage);
